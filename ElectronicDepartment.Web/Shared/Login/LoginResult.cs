using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicDepartment.Web.Shared.Login
{
	public class LoginResult
	{
		public string Message { get; set; }
		public string Email { get; set; }
		public string JWTBearer { get; set; }
		public bool Success { get; set; }
	}

	public class LoginModel
	{
		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Email address is not valid.")]
		public string Email { get; set; } // NOTE: email will be the username, too

		[Required(ErrorMessage = "Password is required.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
