namespace Infractructure.Services
{
    using Newtonsoft.Json;
    using System.Text;
    using System.Net.Http.Headers;
    using Application.Models;
    using Application.Contracts.Abstractions.Services;

    public class ClienteProvider: IClienteProvider
    {
        
        
        /***************************************************************************************************
                                              Consumir servicios NO SEGUROS
         *************************************************************************************************/
        #region Sin Token

        //public async Task<TokenResponseModel> GetToken(string urlBase, string username, string password)
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        client.BaseAddress = new Uri(urlBase);

        //        var ResponseModel = await client.PostAsync("Token",
        //            new StringContent($"grant_type=password&username={username}&password={password}",
        //           Encoding.UTF8, "application/x-www-form-urlencoded"));
               
        //        var resultJSON = await ResponseModel.Content.ReadAsStringAsync();
        //        var result = JsonConvert.DeserializeObject<TokenResponseModel>(
        //            resultJSON);
        //        return result;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        public async Task<ResponseModel> GetList<T>(string urlBase, string path, string queryParams)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}?{1}", path, queryParams);//$"{prefix}{controller}";
                var ResponseModel = await client.GetAsync(url);
                var answer = await ResponseModel.Content.ReadAsStringAsync();
                if (!ResponseModel.IsSuccessStatusCode)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }
                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new ResponseModel
                {
                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = ex.Message

                };
            }
        }

        public async Task<ResponseModel> GetById<T>(string urlBase, string path, int id)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{path}/{id}";
                var ResponseModel = await client.GetAsync(url);
                var answer = await ResponseModel.Content.ReadAsStringAsync();
                if (!ResponseModel.IsSuccessStatusCode)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var item = JsonConvert.DeserializeObject<T>(answer);
                return new ResponseModel
                {
                    IsSuccess = true,
                    Result = item,
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> Post<T>(string urlBase, string path, T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{path}";
                var ResponseModel = await client.PostAsync(url, content);
                var answer = await ResponseModel.Content.ReadAsStringAsync();
                if (!ResponseModel.IsSuccessStatusCode)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new ResponseModel
                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> Put<T>(string urlBase, string path, T model, int id)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{path}/{id}";
                var ResponseModel = await client.PutAsync(url, content);
                var answer = await ResponseModel.Content.ReadAsStringAsync();
                if (!ResponseModel.IsSuccessStatusCode)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new ResponseModel
                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> Delete(string urlBase, string path, int id)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{path}/{id}";
                var ResponseModel = await client.DeleteAsync(url);
                var answer = await ResponseModel.Content.ReadAsStringAsync();
                if (!ResponseModel.IsSuccessStatusCode)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                return new ResponseModel
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        #endregion

        /***************************************************************************************************
                                                    Con token
         *************************************************************************************************/
        #region Con TOKEN
        public async Task<ResponseModel> GetList<T>(string urlBase, string path, string queryParams, string tokenType, string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                //Agregamos el encabezado
                //*********************************************************************************************
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                //*********************************************************************************************
                var url = $"{path}?{queryParams}";
                var ResponseModel = await client.GetAsync(url);
                var answer = await ResponseModel.Content.ReadAsStringAsync();
                if (!ResponseModel.IsSuccessStatusCode)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new ResponseModel
                {
                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> GetById<T>(string urlBase, string path, int id, string tokenType, string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                //Agregamos el encabezado
                //*********************************************************************************************
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                //*********************************************************************************************
                var url = $"{path}/{id}";
                var ResponseModel = await client.GetAsync(url);
                var answer = await ResponseModel.Content.ReadAsStringAsync();
                if (!ResponseModel.IsSuccessStatusCode)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var list = JsonConvert.DeserializeObject<T>(answer);
                return new ResponseModel
                {
                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> Post<T>(string urlBase, string path, T model, string tokenType, string accessToken)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                //Agregamos el encabezado
                //*********************************************************************************************
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                //*********************************************************************************************
                var url = $"{path}";
                var ResponseModel = await client.PostAsync(url, content);
                var answer = await ResponseModel.Content.ReadAsStringAsync();
                if (!ResponseModel.IsSuccessStatusCode)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new ResponseModel
                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> Put<T>(string urlBase, string path, T model, int id, string tokenType, string accessToken)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                //Agregamos el encabezado
                //*********************************************************************************************
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                //*********************************************************************************************
                var url = $"{path}/{id}";
                var ResponseModel = await client.PutAsync(url, content);
                var answer = await ResponseModel.Content.ReadAsStringAsync();
                if (!ResponseModel.IsSuccessStatusCode)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new ResponseModel
                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> Delete(string urlBase, string path, int id, string tokenType, string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                //Agregamos el encabezado
                //*********************************************************************************************
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                //*********************************************************************************************
                var url = $"{path}/{id}";
                var ResponseModel = await client.DeleteAsync(url);
                var answer = await ResponseModel.Content.ReadAsStringAsync();
                if (!ResponseModel.IsSuccessStatusCode)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                return new ResponseModel
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        
        #endregion


    }
}
