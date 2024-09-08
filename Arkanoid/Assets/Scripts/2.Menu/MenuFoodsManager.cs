using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuFoodsManager : MonoBehaviour
{
    private List<Apple> _foodsMenu;

    public event UnityAction EatingFood;

    private void OnDisable()
    {
        for (int i = 0; i < _foodsMenu.Count; i++)
        {
            _foodsMenu[i].ClickedApple -= OnClickedFood;
        }
    }

    public void SetFoods(List<Apple> foodsMenu, int timeReloadFood)
    {
        _foodsMenu = foodsMenu;

        for (int i = 0; i < _foodsMenu.Count; i++)
        {
            _foodsMenu[i].SetValue(timeReloadFood);
            _foodsMenu[i].ClickedApple += OnClickedFood;
        }
    }

    private void OnClickedFood() => EatingFood?.Invoke();
}