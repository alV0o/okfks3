using CinemaTicketSystem;
namespace okfks3_voroshilov
{
    public class CinemaTicketSystemTests
    {
        const int resultWithoutDiscount = 300;
        const int resultForChild = 0;
        const double resultForChildOver6 = resultWithoutDiscount - resultWithoutDiscount*0.4;
        const double resultForStudents = resultWithoutDiscount - resultWithoutDiscount * 0.2;
        const double resultForOld = resultWithoutDiscount - resultWithoutDiscount * 0.5;
        const double resultOnWednesday = resultWithoutDiscount - resultWithoutDiscount * 0.3;
        const double resultInMorning = resultWithoutDiscount - resultWithoutDiscount * 0.15;

        //тест без скидок
        [Theory]
        [InlineData(18)]//тест минимального возраста
        [InlineData(30)]//тест обычного возраста
        [InlineData(64)]//тест максимального возраста
        public void CalculatePrice_ReturnResultWithoutDiscount_ForDefaultUser(int age)
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = new TicketRequest();
            ticketRequest.Age = age;
            ticketRequest.IsStudent = false;
            ticketRequest.IsVip = false;
            ticketRequest.Day = DayOfWeek.Monday;
            ticketRequest.SessionTime = new TimeSpan(14, 0, 0);
            Assert.Equal(resultWithoutDiscount, ticketPriceCalculator.CalculatePrice(ticketRequest));
        }

        //тест для ребенка до 6 лет
        [Theory]
        [InlineData(1)] //тест минимального возраста
        [InlineData(4)] //тест обычного возраста
        [InlineData(5)] //тест максимального
        public void CalculatePrice_ReturnResultForChild_ForChildUnder6(int age)
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = new TicketRequest();
            ticketRequest.Age = age;
            ticketRequest.IsStudent = false;
            ticketRequest.IsVip = false;
            ticketRequest.Day = DayOfWeek.Monday;
            ticketRequest.SessionTime = new TimeSpan(14, 0, 0);
            Assert.Equal(resultForChild, ticketPriceCalculator.CalculatePrice(ticketRequest));
        }

        //тест для ребенка до 17 больше 6
        [Theory]
        [InlineData(6)] //тест минимального возраста
        [InlineData(10)] //тест обычного возраста
        [InlineData(17)] //тест максимального
        public void CalculatePrice_Return180_ForChildOver6(int age)
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = new TicketRequest();
            ticketRequest.Age = age;
            ticketRequest.IsStudent = false;
            ticketRequest.IsVip = false;
            ticketRequest.Day = DayOfWeek.Monday;
            ticketRequest.SessionTime = new TimeSpan(14, 0, 0);

            Assert.Equal((int)resultForChildOver6, ticketPriceCalculator.CalculatePrice(ticketRequest));
        }

        //тест для студентов от 18 до 25
        [Theory]
        [InlineData(18, 240)] //тест минимального возраста
        [InlineData(20, 240)] //тест обычного возраста
        [InlineData(25, 240)] //тест максимального
        [InlineData(26, 300)] //тест граничного максимального
        [InlineData(30, 300)] // тест студента после 25

        public void CalculatePrice_ReturnPrice_ForStudent(int age, int expected)
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = new TicketRequest();
            ticketRequest.Age = age;
            ticketRequest.IsStudent = true;
            ticketRequest.IsVip = false;
            ticketRequest.Day = DayOfWeek.Monday;
            ticketRequest.SessionTime = new TimeSpan(14, 0, 0);

            Assert.Equal(expected, ticketPriceCalculator.CalculatePrice(ticketRequest));
        }

        //тест среды
        [Fact]
        public void CalculatePrice_Return210_OnWednesday()
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = new TicketRequest();
            ticketRequest.Age = 20;
            ticketRequest.IsStudent = false;
            ticketRequest.IsVip = false;
            ticketRequest.Day = DayOfWeek.Wednesday;
            ticketRequest.SessionTime = new TimeSpan(14, 0, 0);

            Assert.Equal((int)resultOnWednesday, ticketPriceCalculator.CalculatePrice(ticketRequest));
        }

        //тест минимального времени утра
        [Fact]
        public void CalculatePrice_Return255_InMorningIf6()
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = new TicketRequest();
            ticketRequest.Age = 20;
            ticketRequest.IsStudent = false;
            ticketRequest.IsVip = false;
            ticketRequest.Day = DayOfWeek.Monday;
            ticketRequest.SessionTime = new TimeSpan(6, 0, 0);

            Assert.Equal((int)resultInMorning, ticketPriceCalculator.CalculatePrice(ticketRequest));
        }

        //тест неверного времени утра
        [Fact]
        public void CalculatePrice_ReturnResultWithoutDiscount_InMorningIf1()
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = new TicketRequest();
            ticketRequest.Age = 20;
            ticketRequest.IsStudent = false;
            ticketRequest.IsVip = false;
            ticketRequest.Day = DayOfWeek.Monday;
            ticketRequest.SessionTime = new TimeSpan(1, 0, 0);

            Assert.Equal(resultWithoutDiscount, ticketPriceCalculator.CalculatePrice(ticketRequest));
        }

        //тест нормального времени утра
        [Fact]
        public void CalculatePrice_Return255_InMorning()
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = new TicketRequest();
            ticketRequest.Age = 20;
            ticketRequest.IsStudent = false;
            ticketRequest.IsVip = false;
            ticketRequest.Day = DayOfWeek.Monday;
            ticketRequest.SessionTime = new TimeSpan(10, 0, 0);

            Assert.Equal((int)resultInMorning, ticketPriceCalculator.CalculatePrice(ticketRequest));
        }

        //тест максимального времени утра
        [Fact]
        public void CalculatePrice_Return255_InMorningIfMax()
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = new TicketRequest();
            ticketRequest.Age = 20;
            ticketRequest.IsStudent = false;
            ticketRequest.IsVip = false;
            ticketRequest.Day = DayOfWeek.Monday;
            ticketRequest.SessionTime = new TimeSpan(11, 30, 0);

            Assert.Equal((int)resultInMorning, ticketPriceCalculator.CalculatePrice(ticketRequest));
        }

        //тест больше максимального времени утра
        [Fact]
        public void CalculatePrice_ReturnresultWithoutDiscount_InMorningOverMax()
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = new TicketRequest();
            ticketRequest.Age = 20;
            ticketRequest.IsStudent = false;
            ticketRequest.IsVip = false;
            ticketRequest.Day = DayOfWeek.Monday;
            ticketRequest.SessionTime = new TimeSpan(12, 0, 0);

            Assert.Equal(resultWithoutDiscount, ticketPriceCalculator.CalculatePrice(ticketRequest));
        }


        [Theory]
        [InlineData(4, 0)] //тест випа для ребенка до 6
        [InlineData(30, 600)] //тест випа без скидок
        [InlineData(67, 300)] //тест випа пенсионера
        [InlineData(15, 360)] //тест випа ребенка до 17 больше 6
        public void CalculatePrice_Return_ForVIP(int age, int expected)
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = new TicketRequest();
            ticketRequest.Age = age;
            ticketRequest.IsStudent = false;
            ticketRequest.IsVip = true;
            ticketRequest.Day = DayOfWeek.Monday;
            ticketRequest.SessionTime = new TimeSpan(14, 0, 0);

            Assert.Equal(expected, ticketPriceCalculator.CalculatePrice(ticketRequest));
        }

        //тест для випа студента
        [Fact]
        public void CalculatePrice_Return480_ForVIPStudent()
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = new TicketRequest();
            ticketRequest.Age = 20;
            ticketRequest.IsStudent = true;
            ticketRequest.IsVip = true;
            ticketRequest.Day = DayOfWeek.Monday;
            ticketRequest.SessionTime = new TimeSpan(14, 0, 0);


            Assert.Equal((int)resultForStudents*2, ticketPriceCalculator.CalculatePrice(ticketRequest));
        }

        //тест студента в среду утром
        [Fact]
        public void CalculatePrice_Return480_ForStudentOnWednesdayInMorning()
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = new TicketRequest();
            ticketRequest.Age = 20;
            ticketRequest.IsStudent = true;
            ticketRequest.IsVip = false;
            ticketRequest.Day = DayOfWeek.Wednesday;
            ticketRequest.SessionTime = new TimeSpan(10, 0, 0);

            Assert.Equal((int)resultOnWednesday, ticketPriceCalculator.CalculatePrice(ticketRequest));
        }

        //тест макс скидок со скидкой среды
        [Theory]
        [InlineData(4, 0)] //тест максимальной скидки для ребенка до 6
        [InlineData(67, 150)] //тест максимальной скидки для пенсионера
        [InlineData(15, 180)] //тест максимальной скидки для ребенка до 17
        public void CalculatePrice_Return_ForMaxDiscountOnWednesday(int age, int expected)
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = new TicketRequest();
            ticketRequest.Age = age;
            ticketRequest.IsStudent = false;
            ticketRequest.IsVip = false;
            ticketRequest.Day = DayOfWeek.Wednesday;
            ticketRequest.SessionTime = new TimeSpan(14, 0, 0);

            Assert.Equal(expected, ticketPriceCalculator.CalculatePrice(ticketRequest));
        }

        //тест на выбрасывание исключения для null request
        [Fact]
        public void CalculatePrice_ReturnException_ForNullRequest()
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = null;

            Assert.Throws<ArgumentNullException>(()=> ticketPriceCalculator.CalculatePrice(ticketRequest));
        }

        //тест на выход за границы
        [Theory]
        [InlineData(-1)]//выход за минимальный
        [InlineData(121)]//выход за максимальный
        public void CalculatePrice_ReturnException_ForBadAge(int age)
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = new TicketRequest();
            ticketRequest.Age = age;
            ticketRequest.IsStudent = false;
            ticketRequest.IsVip = false;
            ticketRequest.SessionTime = new TimeSpan(14, 0, 0);
            ticketRequest.Day = DayOfWeek.Friday;

            Assert.Throws<ArgumentOutOfRangeException>(() => ticketPriceCalculator.CalculatePrice(ticketRequest));
        }

        //расчет максимальной границы возраста
        [Fact]
        public void CalculatePrice_Return150_ForMaxBorder()
        {
            TicketPriceCalculator ticketPriceCalculator = new TicketPriceCalculator();

            TicketRequest ticketRequest = new TicketRequest();
            ticketRequest.Age = 120;
            ticketRequest.IsStudent = false;
            ticketRequest.IsVip = false;
            ticketRequest.SessionTime = new TimeSpan(14, 0, 0);
            ticketRequest.Day = DayOfWeek.Friday;
        
            Assert.Equal((int)resultForOld, ticketPriceCalculator.CalculatePrice(ticketRequest));
        }
    }
}