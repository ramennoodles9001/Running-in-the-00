using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public GameObject slotPrefab;
    public int invSize;
    public Item[] inventory;
    public GameObject[] slots;
    public Image[] slotBackground;
    public Image[] slotIcon;
    public Text[] slotQuantity;
	public ItemDatabase idb;
    public GameObject inventoryObj;
    public GameObject options;
    public GameObject parentCanvas;
    public GameObject player;
    public GameObject currentOption;
    public GameObject weaponSocket;
    public GameObject equippedObject;
    public Item equippedWeapon;
    // Use this for initialization
    void Start()
    {   
        weaponSocket = GameObject.FindGameObjectWithTag("WeaponSocket");
        player = GameObject.FindGameObjectWithTag("Player");
		idb = GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<ItemDatabase>();
        inventory = new Item[invSize];
        slots = new GameObject[invSize];
        slotBackground = new Image[invSize];
        slotIcon = new Image[invSize];
        slotQuantity = new Text[invSize];
        inventoryObj = GameObject.FindGameObjectWithTag("InventoryBox");
        if (invSize == 0)
        {
            Debug.Log("Make sure you set invSize in the inventory component.");
        }
        //Initializes all arrays
        for (int x = 0; x < invSize; x++)
        {
            GameObject tempSlot = Instantiate(slotPrefab);
            tempSlot.transform.SetParent(inventoryObj.transform);
            slots[x] = tempSlot;
            slotBackground[x] = tempSlot.GetComponent<Image>();
            slotIcon[x] = tempSlot.GetComponentInChildren<Image>();
            slotQuantity[x] = tempSlot.GetComponentInChildren<Text>();
			inventory[x] = idb.itemdatabase[0];
            
        }
		UpdateInventory();
        int i = 0;
        //Loops through every slot adding an event for when they are clicked on. This calls the method showOptions which displays
        //All the avaiable options on the item in the slot clicked
        foreach(GameObject slot in slots){
            int _i = i++;
            slot.GetComponent<Button>().onClick.AddListener(() => this.showOptions(_i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Spawns gameobject and removes from inventory
    public void Drop(int id){
        GameObject temp = Instantiate(inventory[id].prefab, transform.position, Quaternion.identity);
        if(temp.GetComponent<ItemInfo>()){
            temp.GetComponent<ItemInfo>().setItem(inventory[id]);
        }else{
            ItemInfo i = temp.AddComponent<ItemInfo>();
            i.setItem(inventory[id]);
        }
        inventory[id] = idb.itemdatabase[0];
        UpdateInventory();
    }
    //Equips weapon or switches currently equipped item with previous
    public void Equip(int id){
        if(equippedObject == null){
            equippedWeapon = new Item(inventory[id]);
            equippedObject = Instantiate(inventory[id].prefab, weaponSocket.transform.position,weaponSocket.transform.rotation,weaponSocket.transform);
            inventory[id] = idb.itemdatabase[0];
            if(equippedObject.GetComponent<ItemInfo>()){
                equippedObject.GetComponent<ItemInfo>().Equip();
            }else{
                equippedObject.AddComponent<ItemInfo>();
                equippedObject.GetComponent<ItemInfo>().Equip();
            }
            
        }else{
            Destroy(equippedObject.gameObject);
            equippedObject = Instantiate(inventory[id].prefab, weaponSocket.transform.position,weaponSocket.transform.rotation,weaponSocket.transform);
            AddItem(equippedWeapon);
            equippedWeapon = new Item(inventory[id]);
            inventory[id] = idb.itemdatabase[0];
            if(equippedObject.GetComponent<ItemInfo>()){
                equippedObject.GetComponent<ItemInfo>().Equip();
            }else{
                equippedObject.AddComponent<ItemInfo>();
                equippedObject.GetComponent<ItemInfo>().Equip();
            }
        }
        UpdateInventory();
    }

    //Displays all options for an item
    public void showOptions(int id)
    {   
        destroyOption();
        Vector3 mousepos = Input.mousePosition;
        currentOption = Instantiate(options, Input.mousePosition, Quaternion.identity, parentCanvas.transform );
        currentOption.GetComponent<OptionPanel>().setID(id);
        foreach(OptionPanel.Options o in inventory[id].options){
            currentOption.GetComponent<OptionPanel>().addOption(o);
        }

    }
    //Removes the Options UI
    public void destroyOption(){
        if(currentOption != null)
        Destroy(currentOption.gameObject);
    }
    //Updates the appearance of the inventory
    public void UpdateInventory()
    {
        for (int x = 0; x < invSize; x++)
        {
            slotIcon[x].sprite = inventory[x].icon;
			if(inventory[x].quantity > 0){
            	slotQuantity[x].text = inventory[x].quantity.ToString();
			}else{
				slotQuantity[x].text = "";
			}
		}
    }
    //Adds an item of a quantity of 1.
    //Return 0 = Success, Return x = We have that much quantity left, Return -1 = Unsuccesful
    /*
    
    MAKE SURE YOU SEND AN ITEM BY VALUE NOT REFERENCE, THAT WAY U CAN MAKE CUSTOM ITEMS!!
    
    
     */
    public int AddItem(Item item)
    {
        if (item.stackable)
        {
            if (!haveEmpty())
            {
                //Loops through inventory increasing the quantity if we have space
                for (int i = 0; i < invSize; i++)
                {
                    if (item.id == inventory[i].id && inventory[i].quantity < inventory[i].maxstack)
                    {
                        inventory[i].quantity++;
						UpdateInventory();
                        return 0;
                    }
                }
            }
            else
            {
                if (haveItem(item))
                {
                    //Loops through the inventory increasing the quantity if we have the item. If we at the end of the loop, simply create a new item
                    //This assumes we have space!
                    for (int i = 0; i < invSize; i++)
                    {
                        if (item.id == inventory[i].id && inventory[i].quantity < inventory[i].maxstack)
                        {   
                            inventory[i].quantity++;
							UpdateInventory();
                            return 0;
                        }else{
                            if(i== invSize-1){
                                inventory[getEmpty()] = item;
                                UpdateInventory();
                                return 0;
                            }
                        }
                    }
                }
                else
                {
                    //If we don't have the item, then just add it to the next available slot
                    inventory[getEmpty()] = item;
					UpdateInventory();
                    return 0;
                }
            }
        }
        else
        {
            //If we have an empty slot, add it
            if (haveEmpty())
            {
                inventory[getEmpty()] = item;
				UpdateInventory();
                return 0;
            }
        }
        //If any of the above methods did not work, this gets called
        return -1;
    }
    public int AddItem(Item item, int amount)
    {
        if (item.stackable)
        {
            if (!haveEmpty())
            {
                int x = 0;
                //Loops through inventory adding quantities where needed then returns the left amount
                for (int i = 0; i < invSize; i++)
                {
                    if (item.id == inventory[i].id && inventory[i].quantity < inventory[i].maxstack)
                    {
                        x = inventory[i].maxstack-inventory[i].quantity;
                        inventory[i].quantity += amount;
						amount -=x;
                    }
                }
                UpdateInventory();
				return x;
            }
            else
            {
                if (haveItem(item))
                {
                    //Loops through inventory maxing out all existing items quantities
                    for (int i = 0; i < invSize; i++)
                    {
                        if (item.id == inventory[i].id && inventory[i].quantity <= inventory[i].maxstack)
                        {
                            int l = inventory[i].maxstack-inventory[i].quantity;
                            amount -= inventory[i].maxstack-inventory[i].quantity;
                            inventory[i].quantity += l;
							UpdateInventory();
                        }
                    }
                    //Once we are dont maxing out quantities, we add the item to a new slot and reloop the 
                    //Quantities until we have none left or no space left
                    int p = getEmpty();
                    for(int x = amount; x>0;x--){
                        if(inventory[p].quantity < inventory[p].maxstack){
                            inventory[p].quantity++;
                        }else{
                            if(haveEmpty()){
                                p = getEmpty();
                                inventory[p] = new Item(item);
                            }else{
                                UpdateInventory();
                                return x;
                            }
                        }
                    }
                    UpdateInventory();
                    return 0;
                }
                else
                {
                    //Same thing as above except we only create new items since we have nothing to add to
                    //Basically above without the first loop
                    Debug.Log("here");
                    int p = getEmpty();
                    inventory[p] = new Item(item);
                    inventory[p].quantity = 1; 
                    for(int x = amount-1; x>0;x--){
                        if(inventory[p].quantity < inventory[p].maxstack){
                            inventory[p].quantity++;
                        }else{
                            if(haveEmpty()){
                                p = getEmpty();
                                inventory[p] = new Item(item);
                            }else{
                                Debug.Log(x);
                                return x;
                            }
                           
                        }
                    }
					UpdateInventory();
                    return 0;
                }
            }

        }
        else
        {
            if (haveEmpty())
            {
                inventory[getEmpty()] = item;
				UpdateInventory();
				return 0;
            }else{
				UpdateInventory();
				return -1;
			}
        }
    }

    //Checks if we have an item that isnt maxed out quantity or something like that - don't touch this
    public bool CheckSlots(Item item)
    {
        bool free = false;
        for (int i = 0; i < invSize; i++)
        {
            if (inventory[i].id == 0 && i != invSize)
            {
                free = true;
            }
            if (inventory[i].id == item.id)
            {
                if (inventory[i].quantity < item.maxstack)
                {
                    return true;
                }
            }
        }
        return free;

    }
    //Returns the first empty slot
    public int getEmpty()
    {
        for (int i = 0; i < invSize; i++)
        {
            if (inventory[i].id == 0)
            {
                return i;
            }
        }
        return -1;
    }
    //Checks if there is an empty slot
    public bool haveEmpty()
    {
        for (int i = 0; i < invSize; i++)
        {
            if (inventory[i].id == 0)
            {
                return true;
            }
        }
        return false;
    }
    //Checks if we have an item
    public bool haveItem(Item item)
    {
        foreach (Item x in inventory)
        {
            if (item.id == x.id)
            {
                return true;
            }
        }
        return false;
    }
    
}
