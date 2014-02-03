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
  }
}