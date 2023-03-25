using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDD : MonoBehaviour
{
    public interface IPickupable
    {
        void Pickup();
    }

    public interface ITransportable
    {
        void Transport();
    }

    public interface IInteractable
    {
        void Interact();
    }

    public interface IDamageable
    {
        void TakeDamage(float damage);
    }

    public interface IDestroyable
    {
        void Destroy();
    }

    public interface IMineable
    {
        void Mine();
    }

    public abstract class MapElement
    {
        public Vector2 position;

        public virtual void Start()
        {
            // Initializations
        }

        public virtual void Update()
        {
            // Update logic
        }

        public virtual void OnEnable()
        {
            // Event subscriptions
        }

        public virtual void OnDisable()
        {
            // Event unsubscriptions
        }
    }

    public class Player : MonoBehaviour, ITransportable, IMineable, IInteractable, IDamageable
    {
        public void Transport()
        {
            // Transport logic
        }

        public void Mine()
        {
            // Mine logic
        }

        public void Interact()
        {
            // Interact logic
        }

        public void TakeDamage(float damage)
        {
            // Damage logic
        }
    }

    public class Item : MapElement, IPickupable, IInteractable
    {
        public void Pickup()
        {
            // Pickup logic
        }

        public void Interact()
        {
            // Interact logic
        }
    }

    public class Resource : MapElement, IPickupable, IMineable, IInteractable
    {
        public void Pickup()
        {
            // Pickup logic
        }

        public void Mine()
        {
            // Mine logic
        }

        public void Interact()
        {
            // Interact logic
        }
    }

    public class Building : MapElement, IDestroyable, IInteractable
    {
        public void Destroy()
        {
            // Destroy logic
        }

        public void Interact()
        {
            // Interact logic
        }
    }

    public class Map : MonoBehaviour
    {
        public List<MapElement> elements;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
