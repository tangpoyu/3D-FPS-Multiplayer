using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendUIModel : MonoBehaviour
{
    [SerializeField] private Transform friendItemsContainer;
    [SerializeField] private UIFriendItem uIFriendItem;

    public Transform FriendItemsContainer { get => friendItemsContainer; set => friendItemsContainer = value; }
    public UIFriendItem UIFriendItem { get => uIFriendItem; set => uIFriendItem = value; }
}
