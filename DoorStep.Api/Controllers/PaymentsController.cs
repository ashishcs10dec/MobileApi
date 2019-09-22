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
    public class PaymentsController : ApiController
    {
        private IPaymentsCore _iPaymentsCore;
        public PaymentsController(IPaymentsCore iPaymentsCore)
        {
            this._iPaymentsCore = iPaymentsCore;
        }

        [Route("api/SavePrePaymentDetails")]
        [HttpPost]
        public HttpResponseMessage SavePrePayment(PaymentModule paymentModule)
        {
            var paymentResponse = _iPaymentsCore.PreSavePayment(paymentModule);
            var output = new OutputResponse()
            {
                IsSuccess = paymentResponse
            };
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, output);
            return response;
        }

        [Route("api/SavePostPaymentDetails")]
        [HttpPost]
        public HttpResponseMessage SavePostPayment(PaymentModule paymentModule)
        {
            var paymentResponse = _iPaymentsCore.PostSavePayment(paymentModule);
            var output = new OutputResponse()
            {
                IsSuccess = paymentResponse
            };
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, output);
            return response;
        }

    }
}
