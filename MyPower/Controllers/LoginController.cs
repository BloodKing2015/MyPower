﻿using MyPower.Buiness;
using MyPower.Model;
using MyPower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPower.Controllers
{
    public class LoginController : MyController
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AccountLogin(UsrLoginModel model)
        {
            ActionResult result = null;
            if (model != null)
            {
                SessionUser SUser = Base_UsrBLL.Instance(baseContext).GetByAccountPwd(model.Account, model.pwd);
                if (SUser != null)
                {
                    SetSessionUser(SUser);
                    Response.Redirect(HomeURL);
                }
            }
            return result;
        }
	}
}