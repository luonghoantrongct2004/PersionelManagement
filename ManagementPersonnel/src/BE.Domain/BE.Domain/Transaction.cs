namespace BE.Domain;

public record Transaction(
    int Id,
    decimal Amount,         // Số tiền
    string Category,        // Danh mục (ăn uống, đi lại...)
    string Type,            // Loại (Income - Thu nhập, Expense - Chi tiêu)
    DateTime Date,          // Ngày giao dịch
    string? Note,           // Ghi chú (nullable)
    Guid UserId              // Liên kết với người dùng
);