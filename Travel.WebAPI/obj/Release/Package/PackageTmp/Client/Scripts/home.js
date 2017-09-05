$(function () {

    $('.home-blogs-slider').slick({
        dots: true,
        infinite: false,
        speed: 300,
        slidesToShow: 5,
        slidesToScroll: 1,
        lazyLoad: 'ondemand',
        responsive: [
          {
              breakpoint: 1024,
              settings: {
                  slidesToShow: 4 
              }
          },
          {
              breakpoint: 800,
              settings: {
                  slidesToShow: 3            
              }
          },
          {
              breakpoint: 600,
              settings: {
                  slidesToShow: 2,
              }
          }
          // You can unslick at a given breakpoint now by adding:
          // settings: "unslick"
          // instead of a settings object
        ]
    });
});