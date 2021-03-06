﻿<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select o.* from str_discountquantity o inner join str_discountgroup g on g.groupid = o.groupid where g.giveawayid is null or g.giveawayid in (select p.id from str_product p inner join str_category c on c.id = p.categoryid where not deleted and (p.parentid is null or p.parentid in (select id from str_product where not deleted)))')">
          <discountquantity>
            <discountquantityid m:left="20" m:top="161">
              <xsl:value-of select="quantityid" />
            </discountquantityid>
            <groupid m:left="-107" m:top="177">
              <xsl:value-of select="groupid" />
            </groupid>
            <quantity m:left="20" m:top="193">
              <xsl:value-of select="quantity" />
            </quantity>
            <percentage m:left="-108" m:top="209">
              <xsl:value-of select="percentage" />
            </percentage>
            <amount m:left="20" m:top="225">
              <xsl:value-of select="amount" />
            </amount>
          </discountquantity>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>