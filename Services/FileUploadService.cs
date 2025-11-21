using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ContractMonthlyClaimSystem.Services
{
    public interface IFileUploadService
    {
        Task<(bool Success, string FileName, string FilePath, string ErrorMessage)> UploadFileAsync(IFormFile file, int claimId);
        bool DeleteFile(string filePath);
        bool IsValidFileType(string fileName);
        bool IsValidFileSize(long fileSize);
        string GetFileExtension(string fileName);
    }

    public class FileUploadService : IFileUploadService
    {
        private readonly string _uploadPath;
        private readonly long _maxFileSize = 10 * 1024 * 1024; // 10MB
        private readonly string[] _allowedExtensions = { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".jpg", ".jpeg", ".png" };

        public FileUploadService()
        {
            try
            {
                _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "claims");
                
                // Ensure upload directory exists
                if (!Directory.Exists(_uploadPath))
                {
                    Directory.CreateDirectory(_uploadPath);
                }
            }
            catch (Exception)
            {
                // Log the error or handle it appropriately
                // For now, we'll use a fallback path
                _uploadPath = Path.Combine(Path.GetTempPath(), "claims_uploads");
                if (!Directory.Exists(_uploadPath))
                {
                    Directory.CreateDirectory(_uploadPath);
                }
            }
        }

        public async Task<(bool Success, string FileName, string FilePath, string ErrorMessage)> UploadFileAsync(IFormFile file, int claimId)
        {
            try
            {
                // Validate file
                if (file == null || file.Length == 0)
                {
                    return (false, "", "", "No file selected.");
                }

                if (!IsValidFileType(file.FileName))
                {
                    return (false, "", "", "Invalid file type. Only PDF, DOC, DOCX, XLS, XLSX, JPG, JPEG, PNG files are allowed.");
                }

                if (!IsValidFileSize(file.Length))
                {
                    return (false, "", "", $"File size exceeds the maximum limit of {_maxFileSize / (1024 * 1024)}MB.");
                }

                // Generate secure filename
                var fileExtension = GetFileExtension(file.FileName);
                var secureFileName = GenerateSecureFileName(file.FileName, claimId);
                var fileName = $"{secureFileName}{fileExtension}";

                // Create claim-specific directory
                var claimDirectory = Path.Combine(_uploadPath, claimId.ToString());
                if (!Directory.Exists(claimDirectory))
                {
                    Directory.CreateDirectory(claimDirectory);
                }

                var filePath = Path.Combine(claimDirectory, fileName);

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Return relative path for web access
                var relativePath = Path.Combine("uploads", "claims", claimId.ToString(), fileName).Replace("\\", "/");

                return (true, file.FileName, relativePath, "");
            }
            catch (Exception ex)
            {
                return (false, "", "", $"Error uploading file: {ex.Message}");
            }
        }

        public bool DeleteFile(string filePath)
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidFileType(string fileName)
        {
            var extension = GetFileExtension(fileName).ToLowerInvariant();
            return _allowedExtensions.Contains(extension);
        }

        public bool IsValidFileSize(long fileSize)
        {
            return fileSize <= _maxFileSize;
        }

        public string GetFileExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        private string GenerateSecureFileName(string originalFileName, int claimId)
        {
            
            var nameWithoutExtension = Path.GetFileNameWithoutExtension(originalFileName);
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var hash = ComputeHash($"{claimId}_{nameWithoutExtension}_{timestamp}");
            
            return $"{claimId}_{timestamp}_{hash.Substring(0, 8)}";
        }

        private string ComputeHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToHexString(bytes).ToLowerInvariant();
            }
        }
    }
}
