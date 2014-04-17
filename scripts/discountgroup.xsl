<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select * from str_discountgroup where giveawayid is null or giveawayid in (select id from str_product where not deleted and producttype = 0 and (parentid is null or parentid in (select id from str_product where not deleted and producttype = 0)))')">
          <discountgroup>
            <discountgroupid m:left="-43" m:top="161">
              <xsl:value-of select="groupid" />
            </discountgroupid>
            <discountgroupname m:left="-166" m:top="177">
              <xsl:value-of select="name" />
            </discountgroupname>
            <businessid m:left="-40" m:top="193">
              <xsl:value-of select="businessid" />
            </businessid>
            <discounttypeid m:left="-166" m:top="208">
              <xsl:value-of select="discounttypeid" />
            </discounttypeid>
            <amount m:left="-39" m:top="225">
              <xsl:value-of select="amount" />
            </amount>
            <percentage m:left="-166" m:top="242">
              <xsl:value-of select="percentage" />
            </percentage>
            <giveawayid m:left="-39" m:top="257">
              <xsl:value-of select="giveawayid" />
            </giveawayid>
            <startdate m:left="-165" m:top="273">
              <xsl:value-of select="startdate" />
            </startdate>
            <expirationdate m:left="-37" m:top="288">
              <xsl:value-of select="expirationdate" />
            </expirationdate>
            <minimumordersum m:left="-166" m:top="305">
              <xsl:value-of select="minimumordersum" />
            </minimumordersum>
            <iscoderange m:left="-38" m:top="323">
              <xsl:value-of select="iscoderange" />
            </iscoderange>
            <codequantity m:left="-166" m:top="338">
              <xsl:value-of select="codequantity" />
            </codequantity>
            <x4ypay m:left="-39" m:top="354">
              <xsl:value-of select="x4ypay" />
            </x4ypay>
            <x4yget m:left="-166" m:top="369">
              <xsl:value-of select="x4yget" />
            </x4yget>
          </discountgroup>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>