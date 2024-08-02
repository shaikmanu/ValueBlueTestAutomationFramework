using RestSharp;

namespace Api.Automation;
public class ApiClient : IApiClient, IDisposable
{
    private readonly RestClient _client;
    private readonly string BASE_URL = "https://api.restful-api.dev/";

    public ApiClient()
    {
        var options = new RestClientOptions(BASE_URL);
        _client = new RestClient(options);
    }

    public async Task<RestResponse> AddObjectAsync<T>(T payload) where T : class
    {
        var request = new RestRequest(Endpoints.ADD_OBJECT, Method.Post);
        request.AddBody(payload);
        return await _client.ExecuteAsync<T>(request);
    }

    public async Task<RestResponse> DeleteObjectAsync(string id)
    {
        var request = new RestRequest(Endpoints.DELETE_OBJECT, Method.Delete);
        var objectId = int.Parse(id);
        request.AddUrlSegment("id", objectId);
        return await _client.ExecuteAsync(request);
    }

    public async Task<RestResponse> GetListOfObjectByIdsAsync(List<int> ids)
    {
        var request = new RestRequest(Endpoints.GET_LIST_OF_BYIDS_OBJECT, Method.Get);

        foreach (var id in ids)
        {
            request.AddQueryParameter("id", id);
        }

        return await _client.ExecuteAsync(request);
    }

    public async Task<RestResponse> GetListOfObjectAsync()
    {
        var request = new RestRequest(Endpoints.GET_LIST_OF_OBJECT, Method.Get);
        return await _client.ExecuteAsync(request);
    }

    public async Task<RestResponse> GetObjectAsync(string id)
    {
        var request = new RestRequest(Endpoints.GET_OBJECT, Method.Get);
        var objectId = int.Parse(id);
        request.AddUrlSegment("id", objectId);
        return await _client.ExecuteAsync(request);
    }

    public async Task<RestResponse> UpdateObjectAsync<T>(T payload, string id) where T : class
    {
        var request = new RestRequest(Endpoints.UPDATE_OBJECT, Method.Put);
        var objectId = int.Parse(id);
        request.AddUrlSegment("id", objectId);
        request.AddBody(payload);
        return await _client.ExecuteAsync<T>(request);
    }

    public async Task<RestResponse> PartialUpdateObjectAsync<T>(T payload, string id) where T : class
    {
        var request = new RestRequest(Endpoints.PARTIALUPDATE_OBJECT, Method.Patch);
        var objectId = int.Parse(id);
        request.AddUrlSegment("id", objectId);
        request.AddBody(payload);
        return await _client.ExecuteAsync<T>(request);
    }

    public void Dispose()
    {
        _client.Dispose();
        GC.SuppressFinalize(this);
    }
}