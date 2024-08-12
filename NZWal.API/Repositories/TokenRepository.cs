using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWal.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            //Create claims
            //Tạo danh sách các Claim(Claim là thông tin bổ sung và chi tiết người dùng)
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //Tạo khóa bảo mật
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])); //Ký token
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Tạo token JWT (JwtSecurityToken):
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),//Thời điểm token hết hạn, ở đây là sau 15 phút.
                signingCredentials: credentials);//Thông tin ký số, để đảm bảo token không bị giả mạo

            return new JwtSecurityTokenHandler().WriteToken(token);
            //Chuyển đổi đối tượng JwtSecurityToken thành chuỗi token JWT đã được ký số
        }
    }
}
