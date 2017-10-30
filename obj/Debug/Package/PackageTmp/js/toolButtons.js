var home = document.createElement('div');
home.className = 'home';
document.body.appendChild(home);
home.innerHTML = '<img src="/images/home.png" />';
var back = document.createElement('div');
back.className = 'back';
document.body.appendChild(back);
back.innerHTML = '<img src="/images/back.png" />';

$('.home').css('width', $('.home').css('height'));
$('.back').css('width', $('.back').css('height'));
$('.home').on('click', function () {
    location.href = '/UserIndex.aspx';
});
$('.back').on('click', function () {
    history.go(-1);
});