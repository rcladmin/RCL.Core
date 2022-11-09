#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using RCL.Core.Azure.Storage;
using RCL.Core.Identity.Graph;

namespace RCL.Core.Identity.Proofing.UI.Areas.IdentityProofing.Pages.UserApproval
{
    [Authorize(Policy = "UserAdmins")]
    public class PhotoModel : PageModel
    {
        private readonly IUserDataService _userDataService;
        private readonly IGraphService _graphService;
        private readonly IAzureBlobStorageService _blobStorage;
        private readonly IOptions<IdentityProofingApiOptions> _options;

        [BindProperty]
        public UserData UserData { get; set; } = new UserData();
        public string ErrorMessage { get; set; } = string.Empty;

        private const string OK = "OK";
        private const string CONTAINER = "rcldid";

        public PhotoModel(IUserDataService userDataService,
            IGraphService graphService,
            IAzureBlobStorageService blobStorage,
            IOptions<IdentityProofingApiOptions> options)
        {
            _userDataService = userDataService;
            _graphService = graphService;
            _blobStorage = blobStorage;
            _options = options;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                await GetUserDataAsync(id);

                if (string.IsNullOrEmpty(UserData?.Email))
                {
                    ErrorMessage = "The user was not found";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile filePhotoUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ErrorMessage = "Data input was not valid";

                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var modelError in modelState.Errors)
                        {
                            ErrorMessage = $"{ErrorMessage},{modelError.ErrorMessage}";
                        }
                    }
                }
                else
                {
                    if (filePhotoUrl != null)
                    {
                        string s = FileChecker(filePhotoUrl);
                        
                        if (s != OK)
                        {
                            ErrorMessage = s;
                            return Page();
                        }

                        string oldBlobName = UserData?.PhotoUrl ?? string.Empty;
                        string newBlobName = await SaveFileAsync(filePhotoUrl);
                        if (string.IsNullOrEmpty(newBlobName))
                        {
                            ErrorMessage = "Could not save proof of address file";
                            return Page();
                        }
                        else
                        {
                            UserData.PhotoUrl = newBlobName;
                            if (!string.IsNullOrEmpty(oldBlobName))
                            {
                                await _blobStorage.DeleteBlobAsync(CONTAINER, oldBlobName);
                            }
                        }
                    }

                    Proofing.UserData updatedUserdata = await _userDataService.UpdateUserDataAsync(_options.Value.SubscriptionId, UserData.Id, UserData);

                    return RedirectToPage("./Details", new  { id = updatedUserdata.Id});

                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }

        private async Task<UserData> GetUserDataAsync(int id)
        {
            try
            {
                UserData = await _userDataService.GetUserDataByIdAsync(_options.Value.SubscriptionId, id);
                return UserData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string FileChecker(IFormFile formfile)
        {
            if (formfile == null)
            {
                return "The file was not uploaded";
            }
            else
            {
                if (formfile.Length < 1)
                {
                    return "File error";
                }
                else
                {
                    if (FileHelper.IsImageFile(formfile.FileName) == false)
                    {
                        return "Only jpg, jpeg, png, gif, bmp, svg image files are allowed";
                    }
                }
            }

            return OK;
        }

        private async Task<string> SaveFileAsync(IFormFile formFile)
        {
            string s = string.Empty;

            try
            {
                string fileExtension = FileHelper.GetFileExtension(formFile.FileName);
                string blobName = $"{Guid.NewGuid().ToString()}{fileExtension}";
                using (var readStream = formFile.OpenReadStream())
                {
                    var blob = await _blobStorage.UploadBlobAsync(CONTAINER, ContainerType.Public, blobName, readStream, FileHelper.GetContentType(fileExtension));
                    if (!string.IsNullOrEmpty(blob?.Uri?.ToString() ?? string.Empty))
                    {
                        s = blob.Uri.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not upload file {ex.Message}");
            }

            return s;
        }
    }
}
