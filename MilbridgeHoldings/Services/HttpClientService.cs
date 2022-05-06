namespace ModelLibrary.Services
{
    using ModelLibrary.UserManagementAPI.Enums;
    using Models.Local;
    using Models.Mappers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Validations;

    public class HttpClientService
    {
        public ActionResult<HttpWebResponse> InvokeRequest<T>(HttpBody<T> request)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(request.Url);
            try
            {
                ActionResult<List<ValidationResult>> checkRequired = DataAnnotationsValidation.Data(request);
                if (!checkRequired.Success) return new ActionResult<HttpWebResponse>
                {
                    Success = checkRequired.Success,
                    Message = checkRequired.Data.Aggregate(string.Empty, (current, result) => current + result.ErrorMessage + Environment.NewLine)
                };

                httpWebRequest.ContentType = request.ContentType.GetEnumDescription();
                httpWebRequest.Method = request.Method.GetEnumDescription();
                if (request.Header != null) Parallel.ForEach(request.Header,
                    header => { httpWebRequest.Headers.Add(header.Name, header.Value); });

                switch (request.Method)
                {
                    case HttpMethod.GET: return Get(httpWebRequest);
                    case HttpMethod.POST:
                        string body = "";
                        switch (request.ContentType)
                        {
                            case HttpContentType.ApplicationJson:
                                body = request.Body.ToJson();
                                break;
                            case HttpContentType.ApplicationXml:
                                body = request.Body.ToXML();
                                break;
                        }
                        return Post(httpWebRequest, body);
                    default: return new ActionResult<HttpWebResponse> { Message = "Specific request method" };
                }
            }
            catch (Exception exception)
            {
                return new ActionResult<HttpWebResponse>
                {
                    Success = false,
                    Message = exception.Message
                };
            }
        }

        private static ActionResult<HttpWebResponse> Post(WebRequest httpWebRequest, string body)
        {
            using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();
            }
            return Get(httpWebRequest);
        }

        private static ActionResult<HttpWebResponse> Get(WebRequest httpWebRequest)
        {
            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream authStream = httpResponse.GetResponseStream();
                if (authStream == null) return new Models.Local.ActionResult<HttpWebResponse> { Message = "No response" };
                using (StreamReader streamReader = new StreamReader(authStream))
                {
                    return new ActionResult<HttpWebResponse>
                    {
                        Success = true,
                        Message = streamReader.ReadToEnd(),
                        Data = httpResponse
                    };
                }
            }
            catch (WebException exception)
            {
                if (exception.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)exception.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            return new ActionResult<HttpWebResponse>
                            {
                                Success = false,
                                Message = reader.ReadToEnd(),
                                Data = (HttpWebResponse)exception.Response
                            };
                        }
                    }
                }
                return new ActionResult<HttpWebResponse>
                {
                    Success = false,
                    Message = exception.Message
                };
            }

            catch (Exception exception)
            {
                return new ActionResult<HttpWebResponse>
                {
                    Success = false,
                    Message = exception.Message
                };
            }

        }
    }
}
