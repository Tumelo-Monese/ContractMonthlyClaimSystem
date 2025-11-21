using Microsoft.AspNetCore.Mvc;
using ContractMonthlyClaimSystem.Models;
using ContractMonthlyClaimSystem.Services;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly IClaimservice _claimService;
        private static List<Document> _documents = new List<Document>();
        private static int _nextDocumentId = 1;

        public ClaimsController(IFileUploadService fileUploadService, IClaimservice claimService)
        {
            _fileUploadService = fileUploadService;
            _claimService = claimService;
        }

        public IActionResult Index()
        {
            var lecturerClaims = _claimService.GetClaimsByLecturerId(1);
            return View(lecturerClaims);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Claim claim, List<IFormFile> uploadedFiles)
        {
           

            if (!ModelState.IsValid)
            {
                return View(claim);
            }
            

            claim.LecturerId = 1; 
                claim.LecturerName = "Jon Doe"; 
                claim.Status = ClaimStatus.Submitted;
                claim.SubmittedOn = DateTime.Now;
                claim.TotalAmount = claim.TotalHours * claim.HourlyRate;
                claim.Documents = new List<Document>();


                if (uploadedFiles != null && uploadedFiles.Count > 0)
                {
                    var uploadedDocuments = new List<Document>();

                    foreach (var file in uploadedFiles)
                    {
                        if (file.Length > 0)
                        {
                            var uploadResult = await _fileUploadService.UploadFileAsync(file, claim.ClaimId);

                            if (uploadResult.Success)
                            {
                                var document = new Document
                                {
                                    DocumentId = _nextDocumentId++,
                                    ClaimId = claim.ClaimId,
                                    FileName = uploadResult.FileName,
                                    FilePath = uploadResult.FilePath,
                                    UploadedOn = DateTime.Now,
                                    FileType = _fileUploadService.GetFileExtension(file.FileName),
                                    FileSize = file.Length,
                                    MimeType = file.ContentType
                                };

                                _documents.Add(document);
                                uploadedDocuments.Add(document);
                            }
                            else
                            {
                                TempData["ErrorMessage"] = uploadResult.ErrorMessage;
                                return View(claim);
                            }
                        }
                    }

                    claim.Documents = uploadedDocuments;
                }

                
                _claimService.AddClaim(claim);

                TempData["SuccessMessage"] = "Claim submitted successfully!";
                return RedirectToAction("Index");
            }

          

        public IActionResult Details(int id)
        {
            var claim = _claimService.GetClaimById(id);
            if (claim == null)
            {
                return NotFound();
            }

            
            claim.Documents = _documents.Where(d => d.ClaimId == id).ToList();

            return View(claim);
        }
    }
}