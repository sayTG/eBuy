using Microsoft.EntityFrameworkCore.Migrations;

namespace eBuy.Data.Migrations
{
    public partial class SeedAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName]) VALUES (N'2fff4f28-70fb-4136-b50c-04dfd14c39ff', N'admin@admin.com', N'ADMIN@ADMIN.COM', N'admin@admin.com', N'ADMIN@ADMIN.COM', 0, N'AQAAAAEAACcQAAAAEEGIUVLD0yXeYR1eR0IWU8UfXa5dqOzSlwWnm2kUSHH55yG8BL7q85PgjhdHNoh4FQ==', N'GU2SXPU7QIA4XTYDLN3XS52WUGWSC6FM', N'3f4e0fc7-8804-428a-99f1-a209661044d4', NULL, 0, 0, NULL, 1, 0, NULL, NULL)

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2fff4f28-70fb-4136-b50c-04dfd14c39ff', N'203ea524-d9a5-415b-b57b-0e3b21f550c2')

");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
