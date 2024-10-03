// 
//  CoyoteErrorCodes.cs
//  
//  Author:
//       Joshua Jackson <jjackson@vortech.net>
// 
//  Date:
//      10/3/2024        
//
//  Product:
//       Coyote Linux https://www.coyotelinux.com
// 	
//  Copyright (c) 1999-2024 Vortech Consulting, LLC, All rights reserved
//
//  This file is part of the Coyote Linux distribution. Please see the Coyote
//  Linux web site for usage and licensing information.

namespace CoyoteLinux.Configuration {
    [Serializable]
    public enum CoyoteErrorCode {
        SUCCESS,
        GENERAL_ERROR,
        ACCESS_DENIED,

        INVALID_OBJECT_NAME,
        DUPLICATE_OBJECT_NAME,

        INVALID_USER_GROUP_NAME,
        DUPLICATE_USER_GROUP_NAME,

        INVALID_USER_NAME,
        DUPLICATE_USER_NAME,

        CONFIG_SAVE_FAILED,
        CONFIG_LOAD_FAILED,

        INTERNAL_ERROR,
        INVALID_GUID_SPECIFIED
    }

    [Serializable]
    public class CoyoteResult {

        public CoyoteErrorCode code;
        public string message;

        public Object resultObj;
        public List<Object> resultList;

        public Exception exception = null;

        public CoyoteResult() {
            code = CoyoteErrorCode.SUCCESS;
            message = GetErrorMessage();
            resultObj = null;
        }

        public CoyoteResult(CoyoteErrorCode aCode) {
            code = aCode;
            message = GetErrorMessage();
            resultObj = null;
        }

        public CoyoteResult(CoyoteErrorCode aCode, Exception ex) {
            code = aCode;
            message = GetErrorMessage();
            exception = ex;
            resultObj = null;
        }

        public CoyoteResult(CoyoteErrorCode aCode, CoyoteConfigSection aResult) {
            code = aCode;
            message = GetErrorMessage();
            resultObj = aResult;
        }

        public CoyoteResult(CoyoteErrorCode aCode, List<Object> aResultList) {
            code = aCode;
            message = GetErrorMessage();
            resultList = aResultList;
        }

        private string GetErrorMessage() {
            switch (code) {
                case CoyoteErrorCode.SUCCESS:
                    return "Success";
                case CoyoteErrorCode.GENERAL_ERROR:
                    return "General Error";
                case CoyoteErrorCode.ACCESS_DENIED:
                    return "Access Denied";

                case CoyoteErrorCode.INVALID_OBJECT_NAME:
                    return "Invalid Object Name Specified";
                case CoyoteErrorCode.DUPLICATE_OBJECT_NAME:
                    return "Duplicate Object Name Specified";

                case CoyoteErrorCode.INVALID_USER_GROUP_NAME:
                    return "Invalid User Group Name";
                case CoyoteErrorCode.DUPLICATE_USER_GROUP_NAME:
                    return "Duplicate User Group Name";

                case CoyoteErrorCode.INVALID_USER_NAME:
                    return "Invalid User Name";
                case CoyoteErrorCode.DUPLICATE_USER_NAME:
                    return "Duplicate User Name";

                case CoyoteErrorCode.INVALID_GUID_SPECIFIED:
                    return "The specified entry GUID was not found or is not valid";

                default:
                    return "Unknown Error Code";
            }
        }
    }

}
