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
    ymaps.route([A, B]).then(
        function (route) {
            var distance = route.getHumanLength(); //Получаем расстояние
            alert(distance.replace(' ', ' '));
            map.geoObjects.add(route); //Рисуем маршрут на карте
        },
        function (error) {
            alert('Ошибка: ' + error.message);
        }
    );
}