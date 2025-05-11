namespace BE.Domain;

public record Category(
    int Id,
    string Name,            // Tên danh mục (ăn uống, đi lại...)
    string Type,            // Loại (Income/Expense)
    int? UserId             // Danh mục tùy chỉnh của người dùng (nullable nếu mặc định)
);