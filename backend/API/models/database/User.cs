using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace backend.models.database
{
    public class User : IdentityUser<int>
    {
        public IEnumerable<UserBook>? UserBooks { get; set; }
    }
}