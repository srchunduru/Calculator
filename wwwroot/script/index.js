const calculator = document.getElementById('calculator');
const keys = calculator.querySelector('.keys');
const display = calculator.querySelector('.result');

var decimal = false;
var valueList = [];

keys.addEventListener('click', e => {
    if (e.target.matches('button')) {
        const key = e.target;
        const action = key.dataset.action;

        const content = key.textContent;
        const number = display.textContent;

        const previousKey = calculator.dataset.previousKey;

        if (!action) {
            calculator.dataset.previousKey = 'number';
            if (display.textContent === '0' || previousKey === 'operator' || previousKey === 'equal') {
                display.textContent = content;
            } else {
                display.textContent = number + content;
            }
        }
        if (action === '.') {
            if (!decimal){
                decimal = true;
                calculator.dataset.previousKey = 'decimal';
                display.textContent = number + '.';
            }
        }
        if (action === '+' || action === '-' || action === '*' || action === '/') {
            calculator.dataset.previousKey = 'operator';
            decimal = false;

            valueList.push(number, action);
            console.log(valueList);
        }
        if (action === 'equal') {
            calculator.dataset.previousKey = 'equal';
            decimal = false;

            if (calculator.dataset.previousKey === 'equal') {
                valueList.push(number);
            }

            calculate().then(result => {
                display.textContent =  result;
            });
            valueList = [];
        }
        if (action === 'clear') {
            calculator.dataset.previousKey = 'clear';
            decimal = false;

            display.textContent = 0;
            valueList = [];
        }
    }
})

async function calculate() {
    console.log(valueList);
    var expressionString = valueList[0];
    valueList.splice(0, 1);
    valueList.forEach(e => {
        expressionString += ' ' + e;
    })

    console.log(expressionString)

    var expressionObj = 
    {
        expression: expressionString
    }

    var expressionBody = new URLSearchParams(expressionObj);

    var result;

    await fetch('http://localhost:9000/api/calculator', {
        method: 'POST',
        body: expressionBody
    }).then(response => {
        console.log(response);
        return response.json();
    }).then(data => {
        result = data[0];
        console.log(result);
        console.log(data[1]);
        return result;
    });

    return result;
}