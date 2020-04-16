var RestMinutes;
var RestSeconds;
var div;

function timerInit() {
    RestMinutes = 19;
    RestSeconds = 59;
    div = document.getElementById('sessionalert');
    div.style.backgroundColor = '#4CAF50';
    div.style.border = 'solid 1px #4CAF50';
    timerUpdate();
}

function timerTick() {
    RestSeconds -= 1;
    if (RestSeconds == 0 && RestMinutes > 0) {
        RestMinutes -= 1;
        RestSeconds = 59;
    }
    if (RestMinutes < 5) {

        div.style.backgroundColor = '#ff9800';
        div.style.border = 'solid 1px #ff9800';
    }
    if (RestMinutes < 3) {

        div.style.backgroundColor = '#f44336';
        div.style.border = 'solid 1px #f44336';
    }


    timerUpdate();
}
function timerUpdate() {
    var m = RestMinutes;
    var s = RestSeconds;
    if (RestSeconds < 10)
        s = '0' + RestSeconds;

    if (RestMinutes < 10)
        m = '0' + RestMinutes;

    ASPxLabel1.SetText('Timeout: ' + m + ':' + s);

    if (RestMinutes <= 0 && RestSeconds <= 0) {
        // Session abgelaufen, jetzt muss was passieren ;)
        ASPxTimer1.SetEnabled(false);
        $.confirm(
            {
                title: 'Session abgelaufen',
                content: 'Die Session ist abgelaufen. Sie werden nun zur Startseite weitergeleitet.',
                autoClose: 'confirm|5000',
                theme: 'supervan',
                useBootstrap: false,
                buttons:
                    {
                        confirm: function () {
                            location.href = '/Default.aspx';
                        }
                    }
            }
        );
    }

}