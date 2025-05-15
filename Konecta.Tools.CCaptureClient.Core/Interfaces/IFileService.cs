using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konecta.Tools.CCaptureClient.Core.Interfaces
{
    public interface IFileService
    {
        string ReadFileAsBase64(string filePath);
        string GetFileName(string filePath);
    }
}
