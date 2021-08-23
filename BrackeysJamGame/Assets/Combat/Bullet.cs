using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] float speed = 1f;
	[SerializeField] int damage = 50;
	[SerializeField] float destroySelfAfter = 5f;

	float timer = 0f;

	void Update()
	{
		transform.position += transform.forward * Time.deltaTime * speed;

		timer += Time.deltaTime;

		if (timer > destroySelfAfter)
			Destroy(this);
	}

	void OnTriggerEnter(Collider _other)
	{
		if (_other.gameObject.CompareTag("Enemy"))
		{
			IHealth _healthSystem = _other.gameObject.GetComponent<IHealth>();

			if (_healthSystem != null)
            {
				_healthSystem.DoDamage(damage);
            }
		}

		Destroy(this.gameObject);
	}
}
