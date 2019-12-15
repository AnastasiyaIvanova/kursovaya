ymaps.ready(init);
var suggestView1;
var suggestView2;

function init() {
    suggestView1 = new ymaps.SuggestView('suggest');
    suggestView2 = new ymaps.SuggestView('suggest2');
    var mapDivId = 'map'; //Id контейнера для карты 
    var mapCenter = [55.76, 37.64]; //Координата центра карты по умолчанию
    map = new ymaps.Map(mapDivId, { center: mapCenter, zoom: 10 });
}

function onClick() {
    var A = document.getElementById("suggest").value;
    var B = document.getElementById("suggest2").value;
    var weight = document.getElementById("Weight").value;
    var length = document.getElementById("Length").value;
    var width = document.getElementById("Width").value;
    var height = document.getElementById("Height").value;
    //var elPrice = document.getElementById("price").value;

    ymaps.route([A, B]).then(
        function (route) {
            var distance = route.getHumanLength(); //Получаем расстояние
            
            var s = distance.replace(' ', ' ');
            var x = s.toString();
            var k = x.indexOf("&");
            x = x.substr(0, k);//расстояние
            //Получаем объемный вес
            var v = (length * width * height) / 5000;
            var price = weight;
            if (v > parseFloat(weight)) {
                price = v;
            }
            price = Math.round(price * x);//цена
            //elPrice.value = "1300";
            alert(price);
            map.geoObjects.add(route); //Рисуем маршрут на карте
        },
        function (error) {
            alert('Ошибка: ' + error.message);
        }
    );
}