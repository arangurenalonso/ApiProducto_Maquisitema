namespace Application.Contracts.Abstractions.Services
{
    using Application.Models;

    public interface IClienteProvider
    {

        /***************************************************************************************************
                                              Consumir servicios NO SEGUROS
         *************************************************************************************************/
        #region Sin Token

        //public Task<TokenResponse> GetToken(string urlBase, string username, string password);

        public Task<ResponseModel> GetList<T>(string urlBase, string path, string queryParams);
        public Task<ResponseModel> GetById<T>(string urlBase, string path, int id);

        public Task<ResponseModel> Post<T>(string urlBase, string path, T model);

        public Task<ResponseModel> Put<T>(string urlBase, string path, T model, int id);

        public Task<ResponseModel> Delete(string urlBase, string path, int id);


        #endregion

        /***************************************************************************************************
                                                    Con token
         *************************************************************************************************/
        #region Con TOKEN
        public Task<ResponseModel> GetList<T>(string urlBase, string path, string queryParams, string tokenType, string accessToken);

        public Task<ResponseModel> GetById<T>(string urlBase, string path, int id, string tokenType, string accessToken);

        public Task<ResponseModel> Post<T>(string urlBase, string path, T model, string tokenType, string accessToken);

        public Task<ResponseModel> Put<T>(string urlBase, string path, T model, int id, string tokenType, string accessToken);

        public Task<ResponseModel> Delete(string urlBase, string path, int id, string tokenType, string accessToken);

        #endregion
    }
}
