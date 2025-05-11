namespace BE.Domain;

public record Goal(
     int Id,
     string Name,            // Tên mục tiêu (mua nhà, du lịch...)
     decimal TargetAmount,   // Số tiền cần đạt
     decimal SavedAmount,    // Số tiền đã tiết kiệm
     DateTime StartDate,     // Ngày bắt đầu
     DateTime? EndDate,      // Ngày kết thúc (nullable)
     int UserId              // Liên kết với người dùng
 );