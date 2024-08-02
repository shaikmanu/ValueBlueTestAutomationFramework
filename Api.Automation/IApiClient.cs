using RestSharp;

namespace Api.Automation;

public interface IApiClient
{
    Task<RestResponse> AddObjectAsync<T>(T payload) where T : class;

    Task<RestResponse> UpdateObjectAsync<T>(T payload, string id) where T : class;

    Task<RestResponse> PartialUpdateObjectAsync<T>(T payload, string id) where T : class;

    Task<RestResponse> DeleteObjectAsync(string id);

    Task<RestResponse> GetObjectAsync(string id) ;

    Task<RestResponse> GetListOfObjectAsync() ;

    Task<RestResponse> GetListOfObjectByIdsAsync(List<int> ids);
}