using ExpenseManager.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.Handlers
{
    public class ProfilePictureDataHandler: ProfilePicture, IProfilePictureDataHandler
    {

        private readonly IConfiguration config;
        public ProfilePictureDataHandler(IConfiguration config)
        {
            this.config = config;
        }

    }
}
