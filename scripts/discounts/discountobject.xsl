<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select o.* from str_discountobject o inner join str_discountgroup g on g.groupid = o.groupid where g.giveawayid is null or g.giveawayid in (select id from str_product where not deleted and producttype = 0 and (parentid is null or parentid in (select id from str_product where not deleted and producttype = 0))) ')">
          <discountobject>
            <discountobjectid m:left="-131" m:top="162">
              <xsl:value-of select="objectid" />
            </discountobjectid>
            <groupid m:left="-4" m:top="181">
              <xsl:value-of select="groupid" />
            </groupid>
            <objecttypeid m:left="-131" m:top="199">
              <xsl:value-of select="objecttypeid" />
            </objecttypeid>
            <itemid m:left="-4" m:top="215">
              <xsl:value-of select="itemid" />
            </itemid>
            <amount m:left="-130" m:top="232">
              <xsl:value-of select="amount" />
            </amount>
            <percentage m:left="4" m:top="250">
              <xsl:value-of select="percentage" />
            </percentage>
          </discountobject>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>