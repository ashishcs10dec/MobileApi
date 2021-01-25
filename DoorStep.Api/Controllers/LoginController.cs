using DoorStep.Api.Models;
using DoorStep.Core.Repository;
using DoorStepModel.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace DoorStep.Api.Controllers
{
    public class LoginController : ApiController
    {
        string terms = ConfigurationManager.AppSettings["TAndCUrl"];
        private IMobileApiCore _iMobileApiCore;
        public LoginController(IMobileApiCore iMobileApiCore)
        {
            _iMobileApiCore = iMobileApiCore;
        }

        [Route("api/Authenticate")]
        [HttpPost]
        public IHttpActionResult Authenticate([FromBody] LoginRequest login)
        {
            var loginResponse = new LoginResponse { };
            LoginRequest loginrequest = new LoginRequest { };
            loginrequest.MobileNo = login.MobileNo;
            loginrequest.OTP = login.OTP;

            IHttpActionResult response;
            HttpResponseMessage responseMsg = new HttpResponseMessage();
            bool isUsernamePasswordValid = false;

            if (login != null)
                isUsernamePasswordValid = loginrequest.OTP == "" ? false : true;

            //verify mobile no and otp in db
            var isVerified = _iMobileApiCore.IsMobileNoVerified(loginrequest.MobileNo, loginrequest.OTP);

            // if credentials are valid
            if (isUsernamePasswordValid && isVerified)
            {
                string token = createToken(loginrequest.MobileNo);
                //updating scret token
                var isSaved = _iMobileApiCore.UpdateSecretToken(loginrequest.MobileNo, token);
                //return the token
                var finalResponse = new Message<Token>()
                {
                    StatusCode = "200",
                    ReturnMessage = "Success",
                    Data = new Token() { SecretToken = token }
                };
                return Ok(finalResponse);
            }
            else
            {
                // if credentials are not valid send unauthorized status code in response
                loginResponse.responseMsg.StatusCode = HttpStatusCode.Unauthorized;
                response = ResponseMessage(loginResponse.responseMsg);
                var finalResponse = new Message<Token>()
                {
                    StatusCode = "401",
                    ReturnMessage = loginResponse.responseMsg.ReasonPhrase.ToString()
                };
                return Ok(finalResponse);
            }
        }

        private string createToken(string username)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddYears(7);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            //Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:55898", audience: "http://localhost:55898",
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        [Route("api/SendMobileNo")]
        [HttpPost]
        public IHttpActionResult GetOTP(MobileOtpDetails mobileDetails)
        {
            try
            {
                var isSaveUpdate = _iMobileApiCore.SaveUpdateMobileOtp(mobileDetails);
                var outputResponse = new OutputResponse() { IsSuccess = isSaveUpdate };
                var response = new Message<OutputResponse>()
                {
                    StatusCode = "200",
                    ReturnMessage = "Success",
                    Data = outputResponse
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new Message<OutputResponse>()
                {
                    StatusCode = "400",
                    ReturnMessage = ex.Message
                };
                return Ok(response);
            }
        }

        [Route("api/TermsAndConditions")]
        [HttpPost]
        public IHttpActionResult TermsAndConditions()
        {
            try
            {
                var termsUrl = new TermsAndConditions() { TermsConditions = terms };
                var response = new Message<TermsAndConditions>()
                {
                    StatusCode = "200",
                    ReturnMessage = "Success",
                    Data = termsUrl
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new Message<TermsAndConditions>()
                {
                    StatusCode = "400",
                    ReturnMessage = ex.Message
                };
                return Ok(response);
            }
        }
    }
}