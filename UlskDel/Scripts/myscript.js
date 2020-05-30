ymaps.ready(init);
var suggestView1;
var suggestView2;

function init() {
    suggestView1 = new ymaps.SuggestView('suggest', {boundedBy: [[54.35, 48.24],[54.30, 48.73]]});
    suggestView2 = new ymaps.SuggestView('suggest2', { boundedBy: [[54.35, 48.24], [54.30, 48.73]] });

    var mapDivId = 'map'; //Id контейнера для карты 
    var mapCenter = [55.76, 37.64]; //Координата центра карты по умолчанию
    map = new ymaps.Map(mapDivId, { center: mapCenter, zoom: 10 });
    geocode('#suggest', '#notice');
    geocode('#suggest2', '#notice2');

    // При клике по кнопке запускаем верификацию введёных данных.
    $('#suggest').on('change', function (e) {
        geocode('#suggest', '#notice');
    });
    $('#suggest2').on('change', function (e) {
        geocode('#suggest2', '#notice2');
    });

    function geocode(item, notice) {
        // Забираем запрос из поля ввода.
        var request = $(item).val();
        // Геокодируем введённые данные.
        ymaps.geocode(request).then(function (res) {
            var obj = res.geoObjects.get(0),
                error, hint;
            console.log(obj);
            if (obj) {
                // Об оценке точности ответа геокодера можно прочитать тут: https://tech.yandex.ru/maps/doc/geocoder/desc/reference/precision-docpage/
                switch (obj.properties.get('metaDataProperty.GeocoderMetaData.precision')) {
                    case 'exact':
                        break;
                    case 'number':
                    case 'near':
                    case 'range':
                        error = 'Неточный адрес, требуется уточнение';
                        hint = 'Уточните номер дома';
                        break;
                    case 'street':
                        error = 'Неполный адрес, требуется уточнение';
                        hint = 'Уточните номер дома';
                        break;
                    case 'other':
                    default:
                        error = 'Неточный адрес, требуется уточнение';
                        hint = 'Уточните адрес';
                }
            } else {
                error = 'Адрес не найден';
                hint = 'Уточните адрес';
            }

            // Если геокодер возвращает пустой массив или неточный результат, то показываем ошибку.
            if (error) {
                showError(error, item, notice);
                //showMessage(hint);
            } else {
                showResult(obj, item, notice);
            }
        }, function (e) {
            console.log(e)
        })

    }
    function showResult(obj, item, notice) {
        // Удаляем сообщение об ошибке, если найденный адрес совпадает с поисковым запросом.
        $(item).removeClass('input_error');
        $(notice).css('display', 'none');

        var mapContainer = $('#map'),
            bounds = obj.properties.get('boundedBy'),
            // Рассчитываем видимую область для текущего положения пользователя.
            mapState = ymaps.util.bounds.getCenterAndZoom(
                bounds,
                [mapContainer.width(), mapContainer.height()]
            ),
            // Сохраняем полный адрес для сообщения под картой.
            address = [obj.getCountry(), obj.getAddressLine()].join(', '),
            // Сохраняем укороченный адрес для подписи метки.
            shortAddress = [obj.getThoroughfare(), obj.getPremiseNumber(), obj.getPremise()].join(' ');
        // Убираем контролы с карты.
        mapState.controls = [];
        // Создаём карту.
        createMap(mapState, shortAddress);
        // Выводим сообщение под картой.
        //showMessage(address);
    }

    function showError(message, item, notice) {
        $(notice).text(message);
        $(item).addClass('input_error');
        $(notice).css('display', 'block');
        // Удаляем карту.
        if (map) {
            map.destroy();
            map = null;
        }
    }

    function createMap(state, caption) {
        // Если карта еще не была создана, то создадим ее и добавим метку с адресом.
        if (!map) {
            map = new ymaps.Map('map', state);
            placemark = new ymaps.Placemark(
                map.getCenter(), {
                    iconCaption: caption,
                    balloonContent: caption
                }, {
                    preset: 'islands#redDotIconWithCaption'
                });
            map.geoObjects.add(placemark);
            // Если карта есть, то выставляем новый центр карты и меняем данные и позицию метки в соответствии с найденным адресом.
        } else {
            map.setCenter(state.center, state.zoom);
            placemark.geometry.setCoordinates(state.center);
            placemark.properties.set({ iconCaption: caption, balloonContent: caption });
        }
    }

    function showMessage(message) {
        $('#messageHeader').text('Данные:');
        $('#message').text(message);
    }
}

function onClick() {
    var A = document.getElementById("suggest").value;
    var B = document.getElementById("suggest2").value;
    var weight = document.getElementById("Weight").value;
    var length = document.getElementById("Length").value;
    var width = document.getElementById("Width").value;
    var height = document.getElementById("Height").value;

    if (!A.trim() || !B.trim()) {
        alert("Введите адреса");
    } else {
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
                $('#price').val(price);
                price = price + "Это примерная стоимость доставки.Окончательная установится после взвешивания груза";
                $('#message').text(price);

                map.geoObjects.add(route); //Рисуем маршрут на карте
            },
            function (error) {
                alert('Ошибка: ' + error.message);
            }
        );
    }
}