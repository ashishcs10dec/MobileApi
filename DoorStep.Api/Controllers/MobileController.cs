using DoorStep.Core.Repository;
using DoorStepModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DoorStep.Api.Controllers
{
    [Authorize]
    public class MobileController : ApiController
    {
        private readonly IMobileApiCore _iMobileApiCore;
        private readonly IBikeHomeDetailsCore _iBikeHomeDetails;
        private readonly IBikeDetailsCore _iBikeDetails;
        public MobileController(IMobileApiCore iMobileApiCore, IBikeHomeDetailsCore iBikeHomeDetails, IBikeDetailsCore iBikeDetails)
        {
            _iMobileApiCore = iMobileApiCore;
            _iBikeHomeDetails = iBikeHomeDetails;
            _iBikeDetails = iBikeDetails;
        }

        /// <summary>
        /// Get all bike listing
        /// </summary>
        /// <param name="PageNo"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [Route("api/GetAllBikes")]
        [HttpGet]
        public HttpResponseMessage GetAllBikeDetails(int PageNo, int PageSize)
        {
            try
            {
                var bikeInformationResponse = _iBikeHomeDetails.BikeInfo(PageNo, PageSize);
                var response = new Message<AllBikesPagerModel>()
                {
                    StatusCode = "200",
                    ReturnMessage = "Success",
                    Data = bikeInformationResponse
                };
                HttpResponseMessage finalResponse = Request.CreateResponse(HttpStatusCode.OK, response);
                return finalResponse;
            }
            catch (Exception ex)
            {
                var response = new Message<AllBikesPagerModel>()
                {
                    StatusCode = "400",
                    ReturnMessage = ex.Message
                };
                HttpResponseMessage finalResponse = Request.CreateResponse(HttpStatusCode.BadRequest, response);
                return finalResponse;
            }
        }

        /// <summary>
        /// Get bike details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/GetBikeDetails/{id}")]
        [HttpGet]
        public IHttpActionResult GetBikeDetails(int? id)
        {
            try
            {
                var bikeInformationResponse = _iBikeDetails.GetBikeDetails(id);
                var response = new Message<CoreBikeInformation>()
                {
                    StatusCode = "200",
                    ReturnMessage = "Success",
                    Data = bikeInformationResponse
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new Message<CoreBikeInformation>()
                {
                    StatusCode = "400",
                    ReturnMessage = ex.Message
                };
                return Ok(response);
            }
        }

        /// <summary>
        /// Save and Update user details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userDetails"></param>
        /// <returns></returns>
        [Route("api/SaveUpdateUserDetails")]
        [HttpPost]
        public IHttpActionResult SaveUpdateUserInfo(HttpRequestMessage request, UserDetails userDetails)
        {
            try
            {
                string token;
                HelperMethods.TryRetrieveToken(request, out token);
                userDetails.SecretToken = token;
                var isSaved = _iMobileApiCore.SaveUserDetials(userDetails);
                if (isSaved == "Success")
                {
                    var response = new Message<BikeInfoById>()
                    {
                        StatusCode = "200",
                        ReturnMessage = isSaved
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new Message<BikeInfoById>()
                    {
                        StatusCode = "400",
                        ReturnMessage = isSaved
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var response = new Message<BikeInfoById>()
                {
                    StatusCode = "400",
                    ReturnMessage = ex.Message
                };
                return Ok(response);
            }
        }

        [Route("api/BikeDetailsByIds")]
        [HttpPost]
        public IHttpActionResult BikeDetailsByIds(HttpRequestMessage request, List<BikeInfoByIds> bikeInfoById)
        {
            try
            {
                string token;
                HelperMethods.TryRetrieveToken(request, out token);
                var res = _iBikeDetails.GetBikeDetailsByIds(bikeInfoById);
                if (res != null)
                {
                    var response = new Message<List<BikeHomeModel>>()
                    {
                        StatusCode = "200",
                        ReturnMessage = "Success",
                        Data = res
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new Message<List<BikeHomeModel>>()
                    {
                        StatusCode = "400",
                        ReturnMessage = "No Record Found"
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var response = new Message<BikeInfoById>()
                {
                    StatusCode = "400",
                    ReturnMessage = ex.Message
                };
                return Ok(response);
            }
        }

        #region Cart Api's
        [Route("api/AddToCart")]
        [HttpPost]
        public IHttpActionResult AddToCart(HttpRequestMessage request, BikeInfoById bikeInfoById)
        {
            try
            {
                string token;
                HelperMethods.TryRetrieveToken(request, out token);
                bikeInfoById.SecretToken = token;
                var carts = _iMobileApiCore.AddToCartCore(bikeInfoById);
                if (carts == "Success")
                {
                    var response = new Message<BikeInfoById>()
                    {
                        StatusCode = "200",
                        ReturnMessage = carts
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new Message<BikeInfoById>()
                    {
                        StatusCode = "400",
                        ReturnMessage = carts
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var response = new Message<BikeInfoById>()
                {
                    StatusCode = "400",
                    ReturnMessage = ex.Message
                };
                return Ok(response);
            }
        }

        [Route("api/RemoveCartItem")]
        [HttpPost]
        public IHttpActionResult RemoveFromCart(HttpRequestMessage request, BikeInfoById bikeInfoById)
        {
            try
            {
                string token;
                HelperMethods.TryRetrieveToken(request, out token);
                bikeInfoById.SecretToken = token;
                var carts = _iMobileApiCore.RemovedCart(bikeInfoById);
                if (carts == "Success")
                {
                    var response = new Message<BikeInfoById>()
                    {
                        StatusCode = "200",
                        ReturnMessage = carts
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new Message<BikeInfoById>()
                    {
                        StatusCode = "400",
                        ReturnMessage = carts
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var response = new Message<BikeInfoById>()
                {
                    StatusCode = "400",
                    ReturnMessage = ex.Message
                };
                return Ok(response);
            }
        }
        #endregion
    }
}
