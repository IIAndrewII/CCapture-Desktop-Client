using Konecta.Tools.CCaptureClient.Core.ApiEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konecta.Tools.CCaptureClient.Core.Interfaces
{
    public interface IApiService
    {
        Task<string> GetAuthTokenAsync(AuthCredentials credentials);
        Task<string> SubmitDocumentAsync(DocumentRequest request, string authToken);
        Task<string> CheckVerificationStatusAsync(VerificationStatusRequest request, string authToken);
    }
}
