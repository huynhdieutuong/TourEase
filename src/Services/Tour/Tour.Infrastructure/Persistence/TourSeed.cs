using Microsoft.EntityFrameworkCore;
using Serilog;
using Tour.Domain.Entities;
using Tour.Domain.Entities.Enums;

namespace Tour.Infrastructure.Persistence;
public class TourSeed
{
    private readonly TourDbContext _context;
    private readonly ILogger _logger;

    public TourSeed(TourDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "An error occurred while initialzing the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await SeedDestinationAsync();
            await SeedTourJobAsync();
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task SeedTourJobAsync()
    {
        if (!_context.TourJob.Any() && !_context.TourDetail.Any())
        {
            var tourJobs = new List<TourJob>()
            {
                new TourJob
                {
                    Id = Guid.Parse("cbf25302-b4f0-4ccb-a976-7551bccc9122"),
                    Title = "Đà Lạt",
                    Slug = "da-lat-3-ngay",
                    Days = 3,
                    SalaryPerDay = 500000,
                    Currency = Currency.VND,
                    ExpiredDate = DateTimeOffset.UtcNow.AddDays(7),
                    Status = Status.Live,
                    Detail = new TourDetail
                    {
                        Id = Guid.Parse("d59194e9-4b00-411b-97b9-e7731356f599"),
                        Itinerary = "Ngày thứ 01 : HỒ CHÍ MINH – ĐÀ LẠT\r\nNgày thứ 02 :THAM QUAN ĐÀ LẠT\r\nNgày thứ 03 : ĐÀ LẠT – HỒ CHÍ MINH",
                        ImageUrl = "http://datnuocvietcantho.vn/wp-content/uploads/2020/05/dalat3ngay3dem-1024x600.jpg",
                        Participants = 20,
                        LanguageSpoken = Language.Vietnamese,
                        StartDate = DateTimeOffset.UtcNow.AddDays(8),
                        EndDate = DateTimeOffset.UtcNow.AddDays(11),
                    }
                },
                new TourJob
                {
                    Id = Guid.Parse("72ff4066-e7af-450b-b3c9-1c6e7ec0776b"),
                    Title = "Tây Nguyên - Nam Trung Bộ",
                    Slug = "tay-nguyen-nam-trung-bo-5-ngay",
                    Days = 5,
                    SalaryPerDay = 450000,
                    Currency = Currency.VND,
                    ExpiredDate = DateTimeOffset.UtcNow.AddDays(5),
                    Status = Status.Live,
                    Detail = new TourDetail
                    {
                        Id = Guid.Parse("fa4c1a32-2f7a-4007-859b-030ef10ee855"),
                        Itinerary = "Ngày thứ 01: HỒ CHÍ MINH – ĐÀ LẠT\r\nNgày thứ 02: THAM QUAN ĐÀ LẠT- BUÔN MA THUỘT\r\nNgày thứ 03: BUÔN MA THUỘT – PLEI KU\r\nNgày thứ 04: PLEI KU – BUÔN MA THUỘT\r\nNgày thứ 05: BUÔN MA THUỘT – HỒ CHÍ MINH",
                        ImageUrl = "http://datnuocvietcantho.vn/wp-content/uploads/2019/04/tay-nguyen-5ngay-1024x600.png",
                        Participants = 30,
                        LanguageSpoken = Language.Vietnamese,
                        StartDate = DateTimeOffset.UtcNow.AddDays(7),
                        EndDate = DateTimeOffset.UtcNow.AddDays(12),
                    }
                },
                new TourJob
                {
                    Id = Guid.Parse("daf35d07-8c98-4399-8f17-0511206b5965"),
                    Title = "Nha Trang - Đà Lạt",
                    Slug = "nha-trang-da-lat-5-ngay",
                    Days = 5,
                    SalaryPerDay = 400000,
                    Currency = Currency.VND,
                    ExpiredDate = DateTimeOffset.UtcNow.AddDays(10),
                    Status = Status.Live,
                    Detail = new TourDetail
                    {
                        Id = Guid.Parse("d7ad6422-a17a-45a0-ac43-e19524eaf5a2"),
                        Itinerary = "Ngày thứ 01: HỒ CHÍ MINH - ĐÀ LẠT\r\nNgày thứ 02: THAM QUAN NHA TRANG\r\nNgày thứ 03: NHA TRANG – ĐÀ LẠT\r\nNgày thứ 04: THAM QUAN ĐÀ LẠT\r\nNgày thứ 05: ĐÀ LẠT – HỒ CHÍ MINH",
                        ImageUrl = "http://datnuocvietcantho.vn/wp-content/uploads/2019/04/nha-trang-da-lat-5ngay-1024x600.png",
                        Participants = 40,
                        LanguageSpoken = Language.Vietnamese,
                        StartDate = DateTimeOffset.UtcNow.AddDays(11),
                        EndDate = DateTimeOffset.UtcNow.AddDays(16),
                    }
                },
                new TourJob
                {
                    Id = Guid.Parse("998a7759-6943-469e-bcf2-01a6a39e7258"),
                    Title = "Con Đường Di Sản Miền Trung",
                    Slug = "con-duong-di-san-mien-trung-7-ngay",
                    Days = 7,
                    SalaryPerDay = 600000,
                    Currency = Currency.VND,
                    ExpiredDate = DateTimeOffset.UtcNow.AddDays(4),
                    Status = Status.Live,
                    Detail = new TourDetail
                    {
                        Id = Guid.Parse("e8a52449-5d77-45fd-a53d-82cec6fdca48"),
                        Itinerary = "Ngày thứ 01: HỒ CHÍ MINH - NHA TRANG\r\nNgày thứ 02: NHA TRANG – QUY NHƠN\r\nNgày thứ 03: QUY NHƠN – HỘI AN - ĐÀ NẴNG\r\nNgày thứ 04: THAM QUAN ĐÀ NẴNG – HUẾ\r\nNgày thứ 05: THAM QUAN HUẾ - ĐÀ NẴNG\r\nNgày thứ 06: ĐÀ NẴNG - NHA TRANG\r\nNgày thứ 07: NHA TRANG – HỒ CHÍ MINH",
                        ImageUrl = "http://datnuocvietcantho.vn/wp-content/uploads/2019/04/mien-trung-7ngay-1024x600.png",
                        Participants = 40,
                        LanguageSpoken = Language.Vietnamese,
                        StartDate = DateTimeOffset.UtcNow.AddDays(5),
                        EndDate = DateTimeOffset.UtcNow.AddDays(12),
                    }
                },
                new TourJob
                {
                    Id = Guid.Parse("0d194ccf-ea5e-4235-b388-bcaa80989a19"),
                    Title = "Phú Quốc",
                    Slug = "phu-quoc-4-ngay",
                    Days = 4,
                    SalaryPerDay = 550000,
                    Currency = Currency.VND,
                    ExpiredDate = DateTimeOffset.UtcNow.AddDays(9),
                    Status = Status.Live,
                    Detail = new TourDetail
                    {
                        Id = Guid.Parse("0217a0fd-f99b-4415-b68a-2c0fab1157c6"),
                        Itinerary = "Ngày thứ 01: HỒ CHÍ MINH – HÀ TIÊN – PHÚ QUỐC\r\nNgày thứ 02: THAM QUAN VINPERLAND PHÚ QUỐC\r\nNgày thứ 03: THAM QUAN ĐẢO NGỌC – LẶN NGẮM SAN HÔ\r\nNgày thứ 04: PHÚ QUỐC – HÀ TIÊN – HỒ CHÍ MINH",
                        ImageUrl = "http://datnuocvietcantho.vn/wp-content/uploads/2019/09/phu-quoc-4ngay-4dem-1024x600.jpg",
                        Participants = 45,
                        LanguageSpoken = Language.Vietnamese,
                        StartDate = DateTimeOffset.UtcNow.AddDays(10),
                        EndDate = DateTimeOffset.UtcNow.AddDays(14),
                    }
                },
                new TourJob
                {
                    Id = Guid.Parse("1dc890d0-d8f3-42fc-b464-f96467401113"),
                    Title = "Huế - Đà Nẵng",
                    Slug = "hue-da-nang-4-ngay",
                    Days = 4,
                    SalaryPerDay = 650000,
                    Currency = Currency.VND,
                    ExpiredDate = DateTimeOffset.UtcNow.AddDays(3),
                    Status = Status.Live,
                    Detail = new TourDetail
                    {
                        Id = Guid.Parse("6801632b-4475-46a5-9073-04ee161be552"),
                        Itinerary = "Ngày thứ 01: TP.HCM – HUẾ\r\nNgày thứ 02: THAM QUAN HUẾ - ĐÀ NẴNG\r\nNgày thứ 03: THAM QUAN ĐÀ NẴNG\r\nNgày thứ 04: ĐÀ NẴNG – HỘI AN - TP.HCM",
                        ImageUrl = "http://datnuocvietcantho.vn/wp-content/uploads/2019/09/hue-da-nang-4ngay-3dem-1024x600.jpg",
                        Participants = 10,
                        LanguageSpoken = Language.Vietnamese,
                        StartDate = DateTimeOffset.UtcNow.AddDays(4),
                        EndDate = DateTimeOffset.UtcNow.AddDays(8),
                    }
                },
                new TourJob
                {
                    Id = Guid.Parse("4bb30d8c-8abe-4022-8d9b-bb2cf5ef7f98"),
                    Title = "Thái Lan",
                    Slug = "thai-lan-4-ngay",
                    Days = 4,
                    SalaryPerDay = 700000,
                    Currency = Currency.VND,
                    ExpiredDate = DateTimeOffset.UtcNow.AddDays(9),
                    Status = Status.Live,
                    Detail = new TourDetail
                    {
                        Id = Guid.Parse("cedee40f-b10c-49e1-8566-0184013a6058"),
                        Itinerary = "Ngày thứ 01: TP.HCM – PATTAYA – TIGER ZOO\r\nNgày thứ 02: ĐẢO CORAIL - TRÂN BẢO PHẬT SƠN - CHỢ NỔI\r\nNgày thứ 03: BANGKOK - BUFFET BAIYOKE - SHOPPING\r\nNgày thứ 04: DẠO THUYỀN - CHÙA PHẬT VÀNG - TP.HCM",
                        ImageUrl = "http://datnuocvietcantho.vn/wp-content/uploads/2019/02/thai-lan-4ngay-4dem-1024x600.png",
                        Participants = 25,
                        LanguageSpoken = Language.Vietnamese,
                        StartDate = DateTimeOffset.UtcNow.AddDays(10),
                        EndDate = DateTimeOffset.UtcNow.AddDays(14),
                    }
                },
                new TourJob
                {
                    Id = Guid.Parse("f530aa22-926e-4e86-b279-095b3d112559"),
                    Title = "Campuchia",
                    Slug = "campuchia-4-ngay",
                    Days = 4,
                    SalaryPerDay = 650000,
                    Currency = Currency.VND,
                    ExpiredDate = DateTimeOffset.UtcNow.AddDays(7),
                    Status = Status.Live,
                    Detail = new TourDetail
                    {
                        Id = Guid.Parse("c34bdb2e-5681-4116-9cc6-9201da4e47f8"),
                        Itinerary = "Ngày thứ 01: TP.HCM – HÀ TIÊN – PHNOM PENH\r\nNgày thứ 02: THAM QUAN PHNOM PENH – SIÊM RIỆP\r\nNgày thứ 03: KHÁM PHÁ SIÊM RIỆP\r\nNgày thứ 04: SIÊM RIỆP – HÀ TIÊN – TP.HCM",
                        ImageUrl = "http://datnuocvietcantho.vn/wp-content/uploads/2019/02/campuchia-4ngay-3dem-1024x600.png",
                        Participants = 35,
                        LanguageSpoken = Language.Vietnamese,
                        StartDate = DateTimeOffset.UtcNow.AddDays(8),
                        EndDate = DateTimeOffset.UtcNow.AddDays(12),
                    }
                },
                new TourJob
                {
                    Id = Guid.Parse("6c66260f-94f5-47ec-af15-d52f06f6b4b4"),
                    Title = "Japan Tour",
                    Slug = "japan-tour-6-day",
                    Days = 6,
                    SalaryPerDay = (decimal)49.50,
                    Currency = Currency.USD,
                    ExpiredDate = DateTimeOffset.UtcNow.AddDays(10),
                    Status = Status.Live,
                    Detail = new TourDetail
                    {
                        Id = Guid.Parse("1a52307a-e057-4455-a1ef-c2ae25bf5109"),
                        Itinerary = "Tokyo - Mt Fuji - Hakone - Kyoto - Nara - Osaka",
                        Participants = 20,
                        LanguageSpoken = Language.English,
                        StartDate = DateTimeOffset.UtcNow.AddDays(11),
                        EndDate = DateTimeOffset.UtcNow.AddDays(16),
                    }
                },
                new TourJob
                {
                    Id = Guid.Parse("a545409f-6296-4272-9524-20e08b899135"),
                    Title = "Korea Tour",
                    Slug = "korea-tour-5-day",
                    Days = 5,
                    SalaryPerDay = (decimal)39.25,
                    Currency = Currency.USD,
                    ExpiredDate = DateTimeOffset.UtcNow.AddDays(12),
                    Status = Status.Live,
                    Detail = new TourDetail
                    {
                        Id = Guid.Parse("98edfdc8-5230-4736-b127-b81743026866"),
                        Itinerary = "SEOUL - BUSAN - DEAGU",
                        Participants = 40,
                        LanguageSpoken = Language.English,
                        StartDate = DateTimeOffset.UtcNow.AddDays(13),
                        EndDate = DateTimeOffset.UtcNow.AddDays(18),
                    }
                },
                new TourJob
                {
                    Id = Guid.Parse("4b9ce0ef-4f9f-45fb-9405-91ed8a137bb6"),
                    Title = "Singapore Tour",
                    Slug = "singapore-5-day",
                    Days = 5,
                    SalaryPerDay = (decimal)35.00,
                    Currency = Currency.USD,
                    ExpiredDate = DateTimeOffset.UtcNow.AddDays(6),
                    Status = Status.Live,
                    Detail = new TourDetail
                    {
                        Id = Guid.Parse("126c9972-be71-40c0-bbe6-c26773713399"),
                        Itinerary = "Singapore",
                        Participants = 20,
                        LanguageSpoken = Language.English,
                        StartDate = DateTimeOffset.UtcNow.AddDays(7),
                        EndDate = DateTimeOffset.UtcNow.AddDays(12),
                    }
                },
                new TourJob
                {
                    Id = Guid.Parse("f8441a8c-14f6-443f-9900-dac436db11c2"),
                    Title = "China Tour",
                    Slug = "china-13-day",
                    Days = 13,
                    SalaryPerDay = (decimal)30.00,
                    Currency = Currency.USD,
                    ExpiredDate = DateTimeOffset.UtcNow.AddDays(12),
                    Status = Status.Live,
                    Detail = new TourDetail
                    {
                        Id = Guid.Parse("71fc9d1f-4999-4e6b-b369-1b1b0600f057"),
                        Itinerary = "Beijing-Xi'an-Chengdu-Guilin-Shanghai",
                        Participants = 30,
                        LanguageSpoken = Language.English,
                        StartDate = DateTimeOffset.UtcNow.AddDays(13),
                        EndDate = DateTimeOffset.UtcNow.AddDays(26),
                    }
                },
            };

            var tourDetailDestination = new List<TourDetailDestination>
            {
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("d59194e9-4b00-411b-97b9-e7731356f599"),
                    DestinationId = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("d59194e9-4b00-411b-97b9-e7731356f599"),
                    DestinationId = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("fa4c1a32-2f7a-4007-859b-030ef10ee855"),
                    DestinationId = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("d7ad6422-a17a-45a0-ac43-e19524eaf5a2"),
                    DestinationId = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("e8a52449-5d77-45fd-a53d-82cec6fdca48"),
                    DestinationId = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("0217a0fd-f99b-4415-b68a-2c0fab1157c6"),
                    DestinationId = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("6801632b-4475-46a5-9073-04ee161be552"),
                    DestinationId = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("cedee40f-b10c-49e1-8566-0184013a6058"),
                    DestinationId = Guid.Parse("8c17b892-385f-46d6-a6f8-53f86bd95922")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("c34bdb2e-5681-4116-9cc6-9201da4e47f8"),
                    DestinationId = Guid.Parse("04152885-5685-41c1-9c5d-cee96bd6f27c")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("1a52307a-e057-4455-a1ef-c2ae25bf5109"),
                    DestinationId = Guid.Parse("b1fd200c-6943-4aad-b7c5-13455a5600dc")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("98edfdc8-5230-4736-b127-b81743026866"),
                    DestinationId = Guid.Parse("9b44e682-2387-4717-a600-66666a607a4e")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("126c9972-be71-40c0-bbe6-c26773713399"),
                    DestinationId = Guid.Parse("70d2f410-01cd-4d73-a867-fd088f55772a")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("71fc9d1f-4999-4e6b-b369-1b1b0600f057"),
                    DestinationId = Guid.Parse("3cda70ac-acc4-41ea-a775-00c087fdf3dc")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("d59194e9-4b00-411b-97b9-e7731356f599"),
                    DestinationId = Guid.Parse("86f45a39-22b9-4ccf-8815-15ddd05afae3")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("fa4c1a32-2f7a-4007-859b-030ef10ee855"),
                    DestinationId = Guid.Parse("86f45a39-22b9-4ccf-8815-15ddd05afae3")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("d7ad6422-a17a-45a0-ac43-e19524eaf5a2"),
                    DestinationId = Guid.Parse("86f45a39-22b9-4ccf-8815-15ddd05afae3")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("d7ad6422-a17a-45a0-ac43-e19524eaf5a2"),
                    DestinationId = Guid.Parse("880bf6a2-1dba-4a72-b493-216fac38eed8")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("e8a52449-5d77-45fd-a53d-82cec6fdca48"),
                    DestinationId = Guid.Parse("880bf6a2-1dba-4a72-b493-216fac38eed8")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("e8a52449-5d77-45fd-a53d-82cec6fdca48"),
                    DestinationId = Guid.Parse("f7c578f0-ab97-4ee3-9ae3-961414bbd1d7")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("6801632b-4475-46a5-9073-04ee161be552"),
                    DestinationId = Guid.Parse("f7c578f0-ab97-4ee3-9ae3-961414bbd1d7")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("e8a52449-5d77-45fd-a53d-82cec6fdca48"),
                    DestinationId = Guid.Parse("8838408c-d17a-49d4-b129-65182c8da63e")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("e8a52449-5d77-45fd-a53d-82cec6fdca48"),
                    DestinationId = Guid.Parse("b727b065-f036-47ab-867e-15c5ee4845f5")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("6801632b-4475-46a5-9073-04ee161be552"),
                    DestinationId = Guid.Parse("b727b065-f036-47ab-867e-15c5ee4845f5")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("0217a0fd-f99b-4415-b68a-2c0fab1157c6"),
                    DestinationId = Guid.Parse("42f18c44-484e-4473-86be-c55ddb7772f0")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("cedee40f-b10c-49e1-8566-0184013a6058"),
                    DestinationId = Guid.Parse("24eb0c73-a750-43c2-aaff-93990a979fe8")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("cedee40f-b10c-49e1-8566-0184013a6058"),
                    DestinationId = Guid.Parse("f781bde3-c37c-4f9e-bbdf-e9bf79323013")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("c34bdb2e-5681-4116-9cc6-9201da4e47f8"),
                    DestinationId = Guid.Parse("1fbe1634-53d2-4f29-8f3c-f7eac80014ba")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("c34bdb2e-5681-4116-9cc6-9201da4e47f8"),
                    DestinationId = Guid.Parse("c041c6e5-7a26-4ca0-bd93-9a348eb84acc")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("1a52307a-e057-4455-a1ef-c2ae25bf5109"),
                    DestinationId = Guid.Parse("616af413-26e2-488c-82ad-0aaade658055")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("d59194e9-4b00-411b-97b9-e7731356f599"),
                    DestinationId = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("98edfdc8-5230-4736-b127-b81743026866"),
                    DestinationId = Guid.Parse("3b621bd7-7914-4d72-80f7-e10b68ae0236")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("98edfdc8-5230-4736-b127-b81743026866"),
                    DestinationId = Guid.Parse("ac31192e-ffe3-4e2a-b780-9af1b5bdf215")
                },
                new TourDetailDestination
                {
                    TourDetailId = Guid.Parse("71fc9d1f-4999-4e6b-b369-1b1b0600f057"),
                    DestinationId = Guid.Parse("e4fcf035-a8f8-4080-957a-687011731865")
                }
            };

            await _context.TourJob.AddRangeAsync(tourJobs);
            await _context.TourDetailDestination.AddRangeAsync(tourDetailDestination);
        }
    }

    private async Task SeedDestinationAsync()
    {
        if (!_context.Destination.Any())
        {
            var countries = new List<Destination>()
            {
                new Destination
                {
                    Id = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528"),
                    Name = "Viet Nam",
                    Slug = "viet-nam",
                    Type = DestinationType.Country
                },
                new Destination
                {
                    Id = Guid.Parse("267bba70-1fb1-422a-b332-7403357a2f1f"),
                    Name = "Indonesia",
                    Slug = "indonesia",
                    Type = DestinationType.Country
                },
                new Destination
                {
                    Id = Guid.Parse("aebd84ac-eff1-439e-8839-bb0235e029d6"),
                    Name = "Malaysia",
                    Slug = "malaysia",
                    Type = DestinationType.Country
                },
                new Destination
                {
                    Id = Guid.Parse("8c17b892-385f-46d6-a6f8-53f86bd95922"),
                    Name = "Thailand",
                    Slug = "thailand",
                    Type = DestinationType.Country
                },
                new Destination
                {
                    Id = Guid.Parse("b1fd200c-6943-4aad-b7c5-13455a5600dc"),
                    Name = "Japan",
                    Slug = "japan",
                    Type = DestinationType.Country
                },
                new Destination
                {
                    Id = Guid.Parse("3bf435f4-aea8-4cd2-9c66-15d2cb7c258a"),
                    Name = "Philippines",
                    Slug = "philippines",
                    Type = DestinationType.Country
                },
                new Destination
                {
                    Id = Guid.Parse("9b44e682-2387-4717-a600-66666a607a4e"),
                    Name = "Korea",
                    Slug = "korea",
                    Type = DestinationType.Country
                },
                new Destination
                {
                    Id = Guid.Parse("3cda70ac-acc4-41ea-a775-00c087fdf3dc"),
                    Name = "China",
                    Slug = "china",
                    Type = DestinationType.Country
                },
                new Destination
                {
                    Id = Guid.Parse("70d2f410-01cd-4d73-a867-fd088f55772a"),
                    Name = "Singapore",
                    Slug = "singapore",
                    Type = DestinationType.Country
                },
                new Destination
                {
                    Id = Guid.Parse("04152885-5685-41c1-9c5d-cee96bd6f27c"),
                    Name = "Cambodia",
                    Slug = "cambodia",
                    Type = DestinationType.Country
                }
            };

            var cities = new List<Destination>()
            {
                new Destination
                {
                    Id = Guid.Parse("137fd6e0-9dd9-4fbf-bf3d-8d8dc64dae46"),
                    Name = "Hồ Chí Minh",
                    Slug = "ho-chi-minh",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528"),
                },
                new Destination
                {
                    Id = Guid.Parse("a898703b-3cb6-43a3-a3ad-fe68bb47adff"),
                    Name = "Hà Nội",
                    Slug = "ha-noi",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528"),
                },
                new Destination
                {
                    Id = Guid.Parse("42f18c44-484e-4473-86be-c55ddb7772f0"),
                    Name = "Phú Quốc",
                    Slug = "phu-quoc",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528"),
                },
                new Destination
                {
                    Id = Guid.Parse("b727b065-f036-47ab-867e-15c5ee4845f5"),
                    Name = "Đà Nẵng",
                    Slug = "da-nang",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528"),
                },
                new Destination
                {
                    Id = Guid.Parse("8838408c-d17a-49d4-b129-65182c8da63e"),
                    Name = "Hội An",
                    Slug = "hoi-an",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528"),
                },
                new Destination
                {
                    Id = Guid.Parse("f7c578f0-ab97-4ee3-9ae3-961414bbd1d7"),
                    Name = "Huế",
                    Slug = "hue",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528"),
                },
                new Destination
                {
                    Id = Guid.Parse("880bf6a2-1dba-4a72-b493-216fac38eed8"),
                    Name = "Nha Trang",
                    Slug = "nha-trang",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528"),
                },
                new Destination
                {
                    Id = Guid.Parse("86f45a39-22b9-4ccf-8815-15ddd05afae3"),
                    Name = "Đà Lạt",
                    Slug = "da-lat",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("d64e62a1-23f2-414d-9bdf-30e4d40b3528"),
                },
                new Destination
                {
                    Id = Guid.Parse("24eb0c73-a750-43c2-aaff-93990a979fe8"),
                    Name = "Bangkok",
                    Slug = "bangkok",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("8c17b892-385f-46d6-a6f8-53f86bd95922"),
                },
                new Destination
                {
                    Id = Guid.Parse("f781bde3-c37c-4f9e-bbdf-e9bf79323013"),
                    Name = "Pattaya",
                    Slug = "pattaya",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("8c17b892-385f-46d6-a6f8-53f86bd95922"),
                },
                new Destination
                {
                    Id = Guid.Parse("3d3d8c4d-9803-4db9-a63f-3bf83d3dcda9"),
                    Name = "Phuket",
                    Slug = "phuket",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("8c17b892-385f-46d6-a6f8-53f86bd95922"),
                },
                new Destination
                {
                    Id = Guid.Parse("f990bbb2-a3a8-4ed9-85b5-930939b1ad27"),
                    Name = "Chiang Mai",
                    Slug = "chiang-mai",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("8c17b892-385f-46d6-a6f8-53f86bd95922"),
                },
                new Destination
                {
                    Id = Guid.Parse("1fbe1634-53d2-4f29-8f3c-f7eac80014ba"),
                    Name = "Phnom Penh",
                    Slug = "phnom-penh",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("04152885-5685-41c1-9c5d-cee96bd6f27c"),
                },
                new Destination
                {
                    Id = Guid.Parse("c041c6e5-7a26-4ca0-bd93-9a348eb84acc"),
                    Name = "Siem Reap",
                    Slug = "siem-reap",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("04152885-5685-41c1-9c5d-cee96bd6f27c"),
                },
                new Destination
                {
                    Id = Guid.Parse("e4fcf035-a8f8-4080-957a-687011731865"),
                    Name = "Beijing",
                    Slug = "beijing",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("3cda70ac-acc4-41ea-a775-00c087fdf3dc"),
                },
                new Destination
                {
                    Id = Guid.Parse("616af413-26e2-488c-82ad-0aaade658055"),
                    Name = "Tokyo",
                    Slug = "tokyo",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("b1fd200c-6943-4aad-b7c5-13455a5600dc"),
                },
                new Destination
                {
                    Id = Guid.Parse("b085d824-d3fd-4cea-ab9a-22f7b8fdeca4"),
                    Name = "Yokohama",
                    Slug = "yokohama",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("b1fd200c-6943-4aad-b7c5-13455a5600dc"),
                },
                new Destination
                {
                    Id = Guid.Parse("3b621bd7-7914-4d72-80f7-e10b68ae0236"),
                    Name = "Seoul",
                    Slug = "seoul",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("9b44e682-2387-4717-a600-66666a607a4e"),
                },
                new Destination
                {
                    Id = Guid.Parse("ac31192e-ffe3-4e2a-b780-9af1b5bdf215"),
                    Name = "Busan",
                    Slug = "busan",
                    Type = DestinationType.City,
                    ParentId = Guid.Parse("9b44e682-2387-4717-a600-66666a607a4e"),
                },
            };

            await _context.Destination.AddRangeAsync(countries);
            await _context.Destination.AddRangeAsync(cities);
        }
    }
}
