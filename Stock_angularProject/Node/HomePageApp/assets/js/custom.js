//Sticky Menu
console.log('hi');
$(window).scroll(function () {
    if($(window).scrollTop() > 400) {
      $("#header").addClass('sticky');
    } else {
      $("#header").removeClass('sticky');
    }
  });
//Sticky End

 //Our Campus
 $(document).ready(function () {
    $("#Our_Campus").owlCarousel({
        loop: true,
        margin: 30,
        nav: false,
        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
        dots: true,
        autoplay: true,
        autoplayHoverPause: true,
        responsive: {
            0: { items: 1},
            600: { items: 1 },
            1000: { items: 1 }
        }
    });
});

//What's happening
$(document).ready(function () {
    $("#whatshappening_slider").owlCarousel({
        loop: true,
        margin: 30,
        nav: true,
        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
        dots: true,
        autoplay: true,
        autoplayHoverPause: true,
        responsive: {
            0: { items: 1},
            600: { items: 3 },
            1000: { items: 4 }
        }
    });
});

//What's happening End

//What's happening
$(document).ready(function () {
    $("#partners_logo_slide").owlCarousel({
        loop: true,
        margin: 30,
        nav: true,
        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
        dots: true,
        autoplay: true,
        autoplayHoverPause: true,
        responsive: {
            0: { items: 1},
            600: { items: 3 },
            1000: { items: 4 }
        }
    });
});

//What's happening End
//Gallery
$(document).ready(function () {
    $(".fancybox").fancybox({
        openEffect: "none",
        closeEffect: "none"
    });

    $(".zoom").hover(function () {

        $(this).addClass('transition');
    }, function () {

        $(this).removeClass('transition');
    });
});
//Gallery End
//OwlCarousel End
//Counter
$('.counter-count').each(function () {
    $(this).prop('Counter',0).animate({
        Counter: $(this).text()
    }, {
        duration: 5000,
        easing: 'swing',
        step: function (now) {
            $(this).text(Math.ceil(now));
        }
    });
});
//Counter End
//AOS Animation
 AOS.init();
 //AOS Animation End


 
 //Color Switcher
var colorButtonRed = document.getElementById('Black');
var colorButtonGreen = document.getElementById('Blue');
var colorButtonBlue = document.getElementById('White');

// colorButtonRed.addEventListener('click', function() {
//   changeColor('#0d0d0d');
// });

// colorButtonGreen.addEventListener('click', function() {
//   changeColor('#1174c0');
// });

// colorButtonBlue.addEventListener('click', function() {
//   changeColor('#ffffff');
// });

function changeColor(color) {
  document.body.style.backgroundColor = color;
}

//Color Switcher End

// Font Accesibility
var element = document.getElementById('increaseFont');
if (element) {
    document.getElementById('increaseFont').addEventListener('click', function() {
        changeFontSize(2);
      });
}
var element1 = document.getElementById('decreaseFont');
if (element1) {
    document.getElementById('decreaseFont').addEventListener('click', function() {
        changeFontSize(-2);
      });
}
var element2 = document.getElementById('defaultFont');
if (element2) {
    document.getElementById('defaultFont').addEventListener('click', function() {
        changeFontSize(0);
      });
}





function changeFontSize(change) {
    console.log('kkk');
  var currentFontSize = parseFloat(window.getComputedStyle(document.body).fontSize);
  
  if (change === 0) {
    document.body.style.fontSize = ''; // Set the default font size
  } else {
    var newFontSize = currentFontSize + change + 'px';
    document.body.style.fontSize = newFontSize;
  }
}
// Font Accesibility End

 
 