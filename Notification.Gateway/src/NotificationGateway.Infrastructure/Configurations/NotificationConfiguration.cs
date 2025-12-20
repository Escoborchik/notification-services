using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationGateway.Domain;

namespace NotificationGateway.Infrastructure.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("notifications");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");

        builder.ComplexProperty(v => v.Recipient, db =>
        {

            db.Property(r => r.Phone)
            .HasMaxLength(Constants.MIN_TEXT_LENGTH)
            .HasColumnName("phone");

            db.Property(c => c.Email)
            .HasMaxLength(Constants.MIN_TEXT_LENGTH)
            .HasColumnName("email");

            db.Property(c => c.PushToken)
           .HasMaxLength(Constants.MIN_TEXT_LENGTH)
           .HasColumnName("push_token");

        });

        builder.ComplexProperty(v => v.Content, db =>
        {

            db.Property(c => c.Subject)
            .HasMaxLength(Constants.MIN_TEXT_LENGTH)
            .HasColumnName("subject");

            db.Property(c => c.Text)
            .HasColumnName("text");
        });

        builder.Property(c => c.Status)
            .HasConversion<string>()
            .HasColumnName("status");

        builder.Property(c => c.Channel)
            .HasConversion<string>()
            .HasColumnName("channel");
    }
}
