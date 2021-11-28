using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleImageGallery.Data.Migrations
{
    public partial class Addingblobcontainermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlobContainerId",
                table: "GalleryImages",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "blobContainers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blobContainers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GalleryImages_BlobContainerId",
                table: "GalleryImages",
                column: "BlobContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryImages_blobContainers_BlobContainerId",
                table: "GalleryImages",
                column: "BlobContainerId",
                principalTable: "blobContainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryImages_blobContainers_BlobContainerId",
                table: "GalleryImages");

            migrationBuilder.DropTable(
                name: "blobContainers");

            migrationBuilder.DropIndex(
                name: "IX_GalleryImages_BlobContainerId",
                table: "GalleryImages");

            migrationBuilder.DropColumn(
                name: "BlobContainerId",
                table: "GalleryImages");
        }
    }
}
