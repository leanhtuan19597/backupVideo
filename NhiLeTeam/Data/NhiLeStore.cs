using NhiLeTeam.Models.Dto;

namespace NhiLeTeam.Data
{
    public static class NhiLeStore
    {
        public static List<NhiLeDTO> nhileList = new List<NhiLeDTO>
            {
                new NhiLeDTO {Id = 1, Name = "Le Anh Tuan", Description = "Đẹp trai quá trời"},
                new NhiLeDTO {Id = 2, Name = "Sơn Babe", Description = "Đẹp trai nhất sớm"}
            };
    }
}
