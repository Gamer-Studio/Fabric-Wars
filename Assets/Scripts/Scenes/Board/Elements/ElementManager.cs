using System.Collections.Generic;
using System.Linq;
using FabricWars.Game.Elements;
using FabricWars.Game.Recipes;
using FabricWars.Graphics.W2D;
using FabricWars.Utils.Extensions;
using FabricWars.Utils.KeyBinds;
using SRF;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Tilemaps;

namespace FabricWars.Scenes.Board.Elements
{
    public class ElementManager : MonoBehaviour
    {
        public static ElementManager instance { get; private set; }

        private ObjectPool<ElementSlot> _pool;
        [SerializeField] private GameObject originalSlot;
        [SerializeField] private Transform slotContainer;

        public List<ElementSlot> slots = new();
        [SerializeField] private List<ElementSlot> activeSlots = new();

        [Header("Entity Builder")] public Camera mainCamera;
        public Tilemap tilemap;
        [SerializeField] private Transform objectContainer;

        private void Awake()
        {
            if (instance)
            {
                Destroy(this);
                return;
            }

            // Test
            var dic = new Dictionary<string, KeyCode>();
            JsonUtility.ToJson(dic);
            //

            mainCamera = Camera.main;

            _pool = new ObjectPool<ElementSlot>(
                () => Instantiate(originalSlot, slotContainer).GetComponent<ElementSlot>(),
                slots.Add,
                slot =>
                {
                    activeSlots.Remove(slot);
                    slots.Remove(slot);
                },
                null, false, 1, 10
            );

            KeyBindManager.instance
                .Bind(BindOptions.downOnly, KeyCodeUtils.Numberics)
                .Then(obj =>
                {
                    if (EVIDialogue.isActiveAndEnabled) return;

                    if (!KeyCodeUtils.TryToInt(obj[0], out var val) ||
                        val is 0 or -1 ||
                        val > slots.Count) return;

                    var slot = slots[val - 1];

                    if (Input.GetKey(KeyCode.LeftShift)) slot.Activate(GetElementInputValue(slot));
                    else slot.Activate();

                    if (slot.elementActive) activeSlots.Add(slot);
                    else activeSlots.Remove(slot);
                });

            instance = this;
        }

        [SerializeField] private W2DManager w2dManager;

        private void Update()
        {
            // Entity creation (Element craft)
            if (Input.GetMouseButtonUp(0) && tilemap != null)
            {
                if (w2dManager == null || w2dManager.beforeTarget != null) return;

                var mPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

                if (tilemap.HasTile(mPos.XY(
                        mPos.x < 0 ? mPos.x - 0.5f : mPos.x + 0.5f,
                        mPos.y < 0 ? mPos.y - 0.5f : mPos.y + 0.5f
                    )))
                {
                    var recipes = new List<ScopeRecipe>();

                    foreach (var slot in activeSlots)
                    {
                        if (recipes.Count > 0)
                        {
                            recipes.RemoveAll(recipe =>
                            {
                                foreach (var scope in recipe.scopes)
                                {
                                    if (TryGetSlot(scope.key, out var activeSlot))
                                    {
                                        return activeSlot.activeValue < scope.value;
                                    }

                                    return true;
                                }

                                return false;
                            });
                            break;
                        }

                        foreach (var (scope, recipe) in General.scopeRecipes[slot.element])
                        {
                            if (slot.activeValue >= scope) recipes.Add(recipe);
                        }

                        recipes.AddRange(from pair in General.scopeRecipes[Element.None] select pair.recipe);
                    }

                    if (recipes.Count > 0)
                    {
                        var result = recipes.Random();


                        foreach (var (element, consume) in result.consumes)
                        {
                            AddElementValue(element, -consume);
                        }

                        Instantiate(result.entity, mPos.Z(0), new Quaternion(), objectContainer);
                    }
                }
            }
        }

        public bool TryGetSlot(Element type, out ElementSlot slot)
        {
            slot = slots.FirstOrDefault(attrSlot => attrSlot.element == type);

            return slot != null;
        }

        public bool AddSlot(Element element, int maxValue, out ElementSlot slot)
        {
            slot = null;
            if (slots.Count > 8 || IsSlotExist(element)) return false;

            slot = _pool.Get();
            slot.Init(element, new(0, maxValue, 0));

            return true;
        }

        public void RemoveSlot(Element element)
        {
            if (slots.Count == 0) return;

            if (!IsSlotExist(element)) return;

            var slot = slots.FirstOrDefault(attrSlot => attrSlot.element == element);

            _pool.Release(slot);
        }

        public bool IsSlotExist(Element element)
        {
            return slots.Any(attrSlot => attrSlot.element == element);
        }

        public void SetElementValue(Element element, int value)
        {
            if (TryGetSlot(element, out var slot))
            {
                slot.storage.value = value;
            }
        }

        public void AddElementValue(Element element, int value)
        {
            if (TryGetSlot(element, out var slot))
            {
                slot.storage.value += value;
            }
            else
            {
                if (value > 0 &&
                    AddSlot(element, 10, out slot))
                {
                    slot.storage.value = value;
                }
            }
        }

        // TODO: 이걸로 유저가 설정한 원소(Element) 수량 가져와서  ElementSlot에 해당 값만큼 설정해서 활성화하게 할 생각
        [SerializeField] private ElementValueInputDialogue EVIDialogue;

        private int GetElementInputValue(ElementSlot slot)
        {
            return 0;
        }

        public IEnumerable<(Element element, int value)> GetActiveElements()
        {
            return from slot in activeSlots
                select (slot.element, slot.activeValue);
        }
    }
}