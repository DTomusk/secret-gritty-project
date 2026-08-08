using Domain.CalendarEvents.Entities;
using Xunit;

namespace Domain.UnitTests.CalendarEvents.Entities;

public class CalendarEventTests
{
    [Fact]
    public void Create_ShouldReturnCalendarEventWithCorrectProperties()
    {
        // Arrange
        var name = "Test Event";
        var date = new DateOnly(2024, 6, 1);
        var eventType = CalendarEventType.BookClub;

        // Act
        var calendarEvent = CalendarEvent.Create(name, date, eventType);

        // Assert
        Assert.NotEqual(Guid.Empty, calendarEvent.Id);
        Assert.Equal(name, calendarEvent.Name);
        Assert.Equal(date, calendarEvent.Date);
        Assert.Equal(eventType, calendarEvent.EventType);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_ShouldThrowArgumentException_WhenNameIsNullOrEmpty(string name)
    {
        // Arrange
        var date = new DateOnly(2024, 6, 1);
        var eventType = CalendarEventType.BookClub;
        // Act & Assert
        Assert.Throws<ArgumentException>(() => CalendarEvent.Create(name, date, eventType));
    }
}
