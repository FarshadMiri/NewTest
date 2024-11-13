 
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
 
 <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>

// بررسی وجود زمان باقی‌مانده در localStorage
var timerDuration = localStorage.getItem('timerDuration') ? parseInt(localStorage.getItem('timerDuration')) : 5 * 60;

if (window.location.pathname !== '/Quiz/ShowResult') {
    var countdownTimer = setInterval(function () {
        var minutes = Math.floor(timerDuration / 60);
        var seconds = timerDuration % 60;
        document.getElementById('timer').innerText = minutes + ":" + (seconds < 10 ? "0" : "") + seconds;

        // ذخیره زمان باقی‌مانده در localStorage
        localStorage.setItem('timerDuration', timerDuration);

        if (timerDuration <= 0) {
            clearInterval(countdownTimer);
            localStorage.removeItem('timerDuration'); // حذف زمان پس از پایان
            window.location.href = '/Quiz/Start'; // هدایت پس از پایان زمان
        }

        timerDuration--;
    }, 1000);
}

// ارسال پاسخ و بارگذاری سوال بعدی
function submitAndLoadNextQuestion(nextIndex) {
    var selectedAnswer = $('input[name="answer"]:checked').val();
    var questionId = $('input[name="questionId"]').val();

    if (!selectedAnswer) {
        alert('لطفا یک گزینه را انتخاب کنید!');
        return;
    }

    // ساختن آبجکت مطابق با ViewModel
    var SubmitAnswerViewModel = {
        UserAnswer: selectedAnswer,
        QuestionId: questionId
    };

    // ارسال داده‌ها به صورت JSON
    $.ajax({
        url: '/Quiz/SubmitAnswer',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(SubmitAnswerViewModel),
        success: function () {
            loadQuestion(nextIndex); // بارگذاری سوال بعدی پس از ارسال موفقیت‌آمیز پاسخ
        },
        error: function () {
            alert('خطایی در ثبت پاسخ رخ داد!');
        }
    });
}

// بارگذاری سوال
function loadQuestion(index) {
    // ابتدا سوال را بارگذاری می‌کنیم
    $.ajax({
        url: '/Quiz/GetQuestion',
        type: 'GET',
        data: { index: index },
        success: function (result) {
            $('#questionContainer').html(result);

            // پس از بارگذاری موفق سوال، questionId جدید را از فرم گرفته و سپس پاسخ را بارگذاری می‌کنیم
            var questionId = $('input[name="questionId"]').val();

            // سپس پاسخ قبلی را بارگذاری می‌کنیم
            $.ajax({
                url: '/Quiz/GetAnswer',
                type: 'GET',
                data: { questionId: questionId },
                success: function (data) {
                    if (data.userAnswer === "Yes") {
                        $('#yesAnswer').prop('checked', true);
                    } else if (data.userAnswer === "No") {
                        $('#noAnswer').prop('checked', true);
                    }
                },
                error: function () {
                    alert('خطا در بارگذاری پاسخ');
                }
            });
        },
        error: function () {
            alert('خطا در بارگذاری سوال');
        }
    });
}

// ارسال نهایی و مشاهده نتیجه آزمون
function submitQuiz() {
    var selectedAnswer = $('input[name="answer"]:checked').val();
    var questionId = $('input[name="questionId"]').val();

    if (!selectedAnswer) {
        alert('لطفا یک گزینه را انتخاب کنید!');
        return;
    }

    // ساختن آبجکت مطابق با ViewModel
    var SubmitAnswerViewModel = {
        UserAnswer: selectedAnswer,
        QuestionId: questionId
    };

    // ارسال داده‌ها به صورت JSON
    $.ajax({
        url: '/Quiz/SubmitAnswer',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(SubmitAnswerViewModel),
        success: function () {
            // پس از ثبت موفق، تایمر را متوقف کرده و localStorage را پاک کنید
            clearInterval(countdownTimer);
            localStorage.removeItem('timerDuration');

            // سپس به صفحه نتیجه هدایت شوید
            window.location.href = '/Quiz/ShowResult';
        },
        error: function () {
            alert('خطایی در ثبت پاسخ رخ داد!');
        }
    });
}

