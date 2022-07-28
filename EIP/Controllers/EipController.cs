using EIP.Commons;
using EIP.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace EIP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EipController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string _hostname = "http://34.142.198.16:8088";

        public EipController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        [Route("/tokenauth/authenticate")]
        public async Task<ActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var httpClient = _httpClientFactory.CreateClient();

            try
            {
		        string requestUrl = $"{_hostname}/tokenauth/authenticate";

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(requestUrl),
                    Method = HttpMethod.Post,
                };

                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (content != null && !string.IsNullOrEmpty(content))
                    {
                        return Ok(content);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/getvessels")]
        public async Task<ActionResult> GetVessels(int pageIndex = 1, int pageSize = 10)
        {
            var httpClient = _httpClientFactory.CreateClient();

            try
            {
		        string requestUrl = $"{_hostname}/getvessels?pageIndex={pageIndex}&pageSize={pageSize}";

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(requestUrl),
                    Method = HttpMethod.Get,
                };

                string bearerToken = Request.Headers["Authorization"].ToString();
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", bearerToken);

                var response = await httpClient.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (content != null && !string.IsNullOrEmpty(content))
                    {
                        return Ok(content);              
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Unauthorized();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/getvessels/{countryCode}")]
        public async Task<IActionResult> GetVesselsByCountryAsync(string countryCode, int pageIndex = 1, int pageSize = 10)
        {
            var httpClient = _httpClientFactory.CreateClient();

            try
            {
		        string requestUrl = $"{_hostname}/getvessels/{countryCode}?pageIndex={pageIndex}&pageSize={pageSize}";

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(requestUrl),
                    Method = HttpMethod.Get,
                };

                string bearerToken = Request.Headers["Authorization"].ToString();
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", bearerToken);

                var response = await httpClient.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (content != null && !string.IsNullOrEmpty(content))
                    {
                        return Ok(content);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return Unauthorized();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/getvessel/{mmsi}")]
        public async Task<IActionResult> GetVesselByMMSI(int mmsi, string startDate, string endDate
                    , int pageIndex = 1, int pageSize = 10)
        {
            var httpClient = _httpClientFactory.CreateClient();

            try
            {
		        string requestUrl = $"{_hostname}/getvessel/{mmsi}?startDate={startDate}&endDate={endDate}&pageIndex={pageIndex}&pageSize={pageSize}";

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(requestUrl),
                    Method = HttpMethod.Get,
                };

                string bearerToken = Request.Headers["Authorization"].ToString();
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", bearerToken);

                var response = await httpClient.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (content != null && !string.IsNullOrEmpty(content))
                    {
                        return Ok(content);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return Unauthorized();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/getmethydro")]
        public async Task<IActionResult> GetMethydro(string startDate = null, string endDate = null,
           MetHydroDataType metHydroDataType = MetHydroDataType.Both, int pageIndex = 1, int pageSize = 10)
        {
            var httpClient = _httpClientFactory.CreateClient();

            try
            {
		        string requestUrl = $"{_hostname}/GetMethydro?startDate={startDate}&endDate={endDate}&metHydroDataType={metHydroDataType}&pageIndex={pageIndex}&pageSize={pageSize}";

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(requestUrl),
                    Method = HttpMethod.Get,
                };

                string bearerToken = Request.Headers["Authorization"].ToString();
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", bearerToken);

                var response = await httpClient.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (content != null && !string.IsNullOrEmpty(content))
                    {
                        return Ok(content);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return Unauthorized();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/getvessels/mmdis")]
        public async Task<ActionResult> GetMmdisVessels(string imoNumber, string officialNumber, string vesselId, string vesselName)
        {
            var httpClient = _httpClientFactory.CreateClient();

            try
            {
                string requestUrl = "https://mmdis.marine.gov.my/MMDIS_DIS_STG/spk/getVessel";

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(requestUrl),
                    Method = HttpMethod.Get,
                };

                MmdisVesselsDet mmdisVesselsDet = new MmdisVesselsDet()
                {
                    imoNumber = imoNumber,
                    officialNumber = officialNumber,
                    vesselId = vesselId,
                    vesselName = vesselName
                };

                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(JsonSerializer.Serialize(mmdisVesselsDet), Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (content != null && !string.IsNullOrEmpty(content))
                    {
                        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        return Ok(content);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

