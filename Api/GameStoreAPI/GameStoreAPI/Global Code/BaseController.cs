using GameStoreAPi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPi.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        internal class ReturnObject
        {
            public Int32 ErrorCode { get; set;}
            public String Message { get; set;}
            public Object Result { get; set;}
        }
    }
}
