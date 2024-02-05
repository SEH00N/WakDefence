using System;
using UnityEngine;
using ProjectWak.Focusing;

namespace ProjectWak.Player
{
    public class PlayerFocurser : MonoBehaviour
    {
        [SerializeField] InputReaderSO inputReader = null;

        [Space(10f)]
        [SerializeField] LayerMask focusableLayer = 0;

        public event Action<IFocusable> OnInteractedEvent = null;

        private Camera mainCamera = null;
        
        private IFocusable focusedObject = null;
        public IFocusable FocusedObject => focusedObject;

        private void Awake()
        {
            mainCamera = Camera.main;

            inputReader.OnLeftClickEvent += HandleLeftClick;
            inputReader.OnRightClickEvent += HandleRightClick;
        }

        private void Update()
        {
            DetectFocusableObject();
        }

        private void OnDestroy()
        {
            inputReader.OnLeftClickEvent -= HandleLeftClick;
            inputReader.OnRightClickEvent -= HandleRightClick;
        }

        private void DetectFocusableObject()
        {
            IFocusable other = null;
            Ray ray = mainCamera.ScreenPointToRay(inputReader.MousePosition);
            bool rayResult = Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, focusableLayer);

            if(rayResult)
                hit.transform.TryGetComponent<IFocusable>(out other);

            if(focusedObject != other)
                FocusObject(other);
        }

        private void FocusObject(IFocusable other)
        {
            focusedObject?.OnFocusEnd();
            focusedObject = other;
            focusedObject?.OnFocusBegin();
        }

        private void HandleLeftClick()
        {
            DetectFocusableObject();
            focusedObject?.OnSelected();
        }

        private void HandleRightClick()
        {
            DetectFocusableObject();
            focusedObject?.OnInteracted();
            OnInteractedEvent?.Invoke(focusedObject);
        }
    }
}
