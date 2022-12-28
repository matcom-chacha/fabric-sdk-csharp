﻿
using FabricCaClient.HFBasicTypes;
using Microsoft.Win32;
using System.Text.Json.Nodes;

namespace FabricCaClient
{
    /// <summary>
    ///  A class that encapsulates a set of methods to communicate with Hyperledger Fabric (HF)'s Certificate Authority (CA).
    /// </summary>
    public class CAClient {
        public ICryptoSuite CryptoSuite { get; set; }

        public static readonly string HFCA_CONTEXT_ROOT = "/api/v1/";

        private readonly string url; // find a more suitable name

        private static readonly string HFCA_REGISTER = HFCA_CONTEXT_ROOT + "register";
        private CryptoPrimitives cryptoPrimitives;
        private bool isSSL;
        private Properties properties;

        /// <summary>
        /// Enrolls an identity
        /// </summary>
        /// /// <param name="x"></param>
        /// <returns></returns>
        public void Enroll() { }

        /// <summary>
        /// Reenrolls an identity
        /// </summary>
        public void Reenroll() { }

        private void SetUpSSL() {
            if (cryptoPrimitives == null) {
                try {
                    cryptoPrimitives = new CryptoPrimitives();
                    cryptoPrimitives.init();
                }
                catch (Exception exc) {
                    throw new Exception("Error while setting crypto primitives", exc);
                }
            }

            if (isSSL && registry == null) {
                if( !properties.Contains("pemBytes") && !properties.Contains("pemFile"){
                    byte [] permbytes = (byte[])
                }
            }
            // socket stuff. some funcitonalities are provided to java by apache. Alternatives need to be found for c#
        }

        private JsonObject HttpPost(string url, string body, IUser registrar) {
            throw new NotImplementedException();
            return new JsonObject();
        }

        /// <summary>
        /// Registers an identity
        /// </summary>
        /// /// <param name="x"></param>
        /// <returns></returns>
        public string Register(RegistrationRequest registrationRequest, IUser registrar) {
            if (CryptoSuite == null) // set in cstr
                                     // customize later with proper exceptions
                throw new Exception("Crypto primitives not set");

            if (registrationRequest == null) // ask for enrollmentID after the interface have been defined
                throw new ArgumentException("Enrollment id not set in registration request");

            if (registrar == null)
                throw new ArgumentException("Registrar should be a valid member");

            SetUpSSL();

            try {
                string body = registrationRequest.ToJson();
                // validate if is neccessary to add token
                JsonObject response = HttpPost(url + HFCA_REGISTER, body, registrar);
                string secret = response["secret"]?.GetValue<string>();

                if (secret == null)
                    throw new Exception("Secret not found in response");

                return secret;
            }
            catch (Exception exc) {
                throw new Exception("Error while registering the user {registrar.Name} with url: {url}", exc);
            }
        }

        /// <summary>
        /// Revokes an identity
        /// </summary>
        public void Revoke() { }
    }
}