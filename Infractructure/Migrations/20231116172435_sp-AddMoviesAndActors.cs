using Microsoft.EntityFrameworkCore.Migrations;


namespace Infractructure.Migrations
{
    public partial class spAddMoviesAndActors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[AddMoviesAndActors]
                    @movieId int,
                    @actorId int
                AS
                BEGIN
                     SET NOCOUNT ON;
                        Insert into Billboard(
                               [MoviesId]
                               ,[ActorsId]
                               )
                             Values (@movieId, @actorId)
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
