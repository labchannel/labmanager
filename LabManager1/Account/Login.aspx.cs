using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using LabManager.Models;
using System.Web.Security;

namespace LabManager.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            // OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            // var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            //  if (!String.IsNullOrEmpty(returnUrl))
            // {
            //    RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            // }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            string @u = txtEmail.Text;
            string @p = txtPassword.Text;
            if (IsValid)
            {
                if (Membership.ValidateUser(@u, @p))
                {
                    // Validate the user password
                    if (chkRememberMe.Checked)
                    {
                        FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(txtEmail.Text, true, 10 * 60);
                        String encryptedTkt = FormsAuthentication.Encrypt(tkt);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTkt);
                        cookie.Expires = tkt.Expiration;
                        HttpContext.Current.Request.Cookies.Set(cookie);

                        //  var manager = membership
                        //var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                        // This doen't count login failures towards account lockout
                        // To enable password failures to trigger lockout, change to shouldLockout: true
                        // var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: true);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(txtEmail.Text, false);
                    }
                    if (Roles.IsUserInRole("Admin"))
                    {
                        Response.Redirect("~/Pages/tenantpage.aspx");
                    }
                    if (Roles.IsUserInRole("Manager"))
                    {
                        Response.Redirect("~/Pages/managerpage.aspx");
                    }
                    else
                    {
                        Label1.Text = Convert.ToString(Roles.GetAllRoles());
                        Response.Redirect("~/Pages/staffpage.aspx");

                    }
                }


                else
                {


                    FailureText.Text = "Invalid login attempt";
                    ErrorMessage.Visible = true;



                }

            }
        }
    }
        }
    