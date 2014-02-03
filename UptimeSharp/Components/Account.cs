using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace UptimeSharp
{
  /// <summary>
  /// UptimeClient
  /// </summary>
  public partial class UptimeClient
  {
    private Regex isEmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,10}))$", RegexOptions.IgnoreCase);


    /// <summary>
    /// Determines whether a e-mail is available.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException">Please provide an e-mail address</exception>
    /// <exception cref="System.ArgumentException">Please provide a valid e-mail address</exception>
    public async Task<bool> IsEmailAvailable(string email, CancellationToken cancellationToken = default(CancellationToken))
    {
      if (String.IsNullOrEmpty(email))
      {
        throw new ArgumentNullException("Please provide an e-mail address");
      }

      if (!isEmailRegex.IsMatch(email))
      {
        throw new ArgumentException("Please provide a valid e-mail address");
      }

      return (await AccountRequest("checkUserEmail", cancellationToken, new Dictionary<string, string>()
      {
        { "userEmail", email }
      })).Success;
    }



    /// <summary>
    /// Registers the account.
    /// </summary>
    /// <param name="fullName">The full name (min. 3 chars).</param>
    /// <param name="email">The email address.</param>
    /// <param name="password">The password (min. 6 chars).</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException">Please provide all parameters (username, email and password)</exception>
    /// <exception cref="System.ArgumentException">
    /// Please provide a valid e-mail address
    /// or
    /// Name must be at least 3 characters
    /// or
    /// Username must be at least 6 characters
    /// </exception>
    public async Task<bool> RegisterAccount(string fullName, string email, string password, CancellationToken cancellationToken = default(CancellationToken))
    {
      if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(fullName) || String.IsNullOrEmpty(password))
      {
        throw new ArgumentNullException("Please provide all parameters (username, email and password)");
      }

      if (!isEmailRegex.IsMatch(email))
      {
        throw new ArgumentException("Please provide a valid e-mail address");
      }

      if (fullName.Length < 3)
      {
        throw new ArgumentException("Name must be at least 3 characters");
      }

      if (password.Length < 6)
      {
        throw new ArgumentException("Username must be at least 6 characters");
      }

      return (await AccountRequest("newUser", cancellationToken, new Dictionary<string, string>()
      {
        { "userFirstLastName", fullName },
        { "userEmail", email },
        { "userPassword", password }
      }, true)).Success;
    }
  }
}