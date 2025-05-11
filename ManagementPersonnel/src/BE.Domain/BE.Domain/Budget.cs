namespace BE.Domain;

public record Budget(
    int Id,
    int CategoryId,         // Liên kết với danh mục
    decimal Limit,          // Giới hạn chi tiêu
    DateTime StartDate,     // Ngày bắt đầu
    DateTime EndDate,       // Ngày kết thúc
    int UserId              // Liên kết với người dùng
);