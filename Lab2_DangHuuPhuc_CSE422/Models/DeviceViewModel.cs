using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab2_DangHuuPhuc_CSE422.Models
{
    public class DeviceViewModel
    {
        public IQueryable<Device>? Devices { get; set; }
        public SelectList? Category { get; set; }
        public string? DeviceCategory { get; set; }
        public List<Status>? Statuses { get; set; }
        public string? SearchString { get; set; }
    }
}