using Konecta.Tools.CCaptureClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konecta.Tools.CCaptureClient.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public string ReadFileAsBase64(string filePath)
        {
            // Validate file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file was not found: {filePath}");
            }

            try
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                return Convert.ToBase64String(fileBytes);
            }
            catch (Exception ex)
            {
                throw new IOException($"Error reading file: {ex.Message}", ex);
            }
        }

        public string GetFileName(string filePath)
        {
            try
            {
                return Path.GetFileName(filePath);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Invalid file path: {ex.Message}", ex);
            }
        }
    }
}
