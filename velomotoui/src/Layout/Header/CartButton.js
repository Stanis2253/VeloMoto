import React, {useContext, useState, useEffect} from 'react';
import styles from "./CartButton.module.css";

const CartButton = (props) => {


    return (
        <button className={styles.button} onClick={props.onClick}>
            <span className={styles.icon}>

            </span>
            <span>Корзина</span>
            <span className={styles.badge}>

            </span>
        </button>
    );
}

export default CartButton;